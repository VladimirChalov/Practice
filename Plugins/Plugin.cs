namespace Plugin;

public class PluginLoad : Attribute
    {
        public string[] DependsOn {get;}
        
        public PluginLoad(params string[] dependsOn)
        {
            DependsOn = dependsOn ?? Array.Empty<string>();
        }
    }

       
        public interface Plugin
    {
        void Execute();
    }
