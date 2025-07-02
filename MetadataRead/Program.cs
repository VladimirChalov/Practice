using System;
using System.Reflection;

class Program
{
    static void Main(string[] args)
    {
        Assembly x = Assembly.LoadFrom(args[0]);
        
        foreach (Type y in x.GetTypes())
        {
            Console.WriteLine($"тип: {y.Name}");
            
            foreach (Attribute z in y.GetCustomAttributes())
                Console.WriteLine($"атрибут: {z.GetType().Name}");
            
            foreach (ConstructorInfo a in y.GetConstructors())
            {
                Console.WriteLine($"конструктор: {a.Name}");
                foreach (ParameterInfo b in a.GetParameters())
                    Console.WriteLine($"параметр: {b.ParameterType.Name} {b.Name}");
            }
            
            foreach (MethodInfo c in y.GetMethods())
            {
                if (!c.IsSpecialName)
                {
                    Console.WriteLine($"метод: {c.Name}");
                    foreach (ParameterInfo d in c.GetParameters())
                        Console.WriteLine($"параметр: {d.ParameterType.Name} {d.Name}");
                }
            }
        }
    }
}
