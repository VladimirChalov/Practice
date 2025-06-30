using System;
using System.Reflection;
using System.Linq;

namespace task07
{
    public class DisplayNameAttribute : Attribute
    {
        public string DisplayName {get;}

        public DisplayNameAttribute(string name)
        {
            DisplayName = name;
        }
    }

    public class VersionAttribute : Attribute
    {
        public int Major {get;}
        public int Minor {get;}

        public VersionAttribute(int a, int b)
        {
            Major = a;
            Minor = b;
        }
    }

    [DisplayName("Пример")]
    [Version(1, 0)]
    public class SampleClass
    {
        public SampleClass()
        {
            Text = ""; 
        }

        [DisplayName("Число")]
        public int Number {get; set;}

        [DisplayName("Текст")]
        public string Text {get; set;} 
    }

    public class ReflectionHelper
    {
        public static void PrintTypeInfo(Type input)
        {
            foreach (var x in input.GetCustomAttributes<DisplayNameAttribute>())
                Console.WriteLine(x.DisplayName);

            foreach (var x in input.GetCustomAttributes<VersionAttribute>())
                Console.WriteLine($"{x.Major}.{x.Minor}");

            foreach (var y in input.GetProperties())
                foreach (var x in y.GetCustomAttributes<DisplayNameAttribute>())
                    Console.WriteLine($"{y.Name}:{x.DisplayName}");

            foreach (var method in input.GetMethods())
                foreach (var x in method.GetCustomAttributes<DisplayNameAttribute>())
                    Console.WriteLine($"{method.Name}:{x.DisplayName}");
        }
    }
}
