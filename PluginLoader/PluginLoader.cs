using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace PlugLoad
{
    public class PluginSearch
    {
        private string str;
        
        public PluginSearch(string str) => this.str = str;

        public void Load()
        {
            var list = new List<Type>();
            
            foreach (var f in Directory.GetFiles(str, "*.dll"))
            {
                var a = Assembly.LoadFrom(f);
                foreach (var t in a.GetTypes())
                    if (t.GetCustomAttribute<PluginLoad>() != null)
                        list.Add(t);
            }

            var done = new Hash_Coll<string>();
            var work = true;
            
            while (work && done.Count < list.Count)
            {
                work = false;
                foreach (var p in list)
                {
                    var n = p.Name;
                    if (done.Contains(n)) continue;
                    
                    var atr = p.GetCustomAttribute<PluginLoad>();
                    var ok = atr?.DependsOn?.All(d => done.Contains(d)) ?? true;
                    
                    if (ok)
                    {
                        ((IPlugin)Activator.CreateInstance(p)).Execute();
                        done.Add(n);
                        work = true;
                    }
                }
            }
        }
    }
}
