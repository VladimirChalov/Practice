using System;
using System.Collections.Generic;
using System.Reflection;

namespace task05
{
  
public class ClassAnalyzer
{
    private Type type;

    public ClassAnalyzer(Type t)
    {
        type = t;
    }

    public List<string> GetPublicMethods()
    {
        var methods = type.GetMethods();
        var result = new List<string>();
        
        foreach (var method in methods)
        {
            if (method.IsPublic)
            {
                result.Add(method.Name);
            }
        }
        
        return result;
    }

    public List<string> GetMethodParams(string methodName)
    {
        var method = type.GetMethod(methodName);
        var result = new List<string>();
        
        if (method != null)
        {
            foreach (var param in method.GetParameters())
            {
                result.Add(param.Name);
            }
        }
        
        return result;
    }

    public List<string> GetAllFields()
    {
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        var result = new List<string>();
        
        foreach (var field in fields)
        {
            result.Add(field.Name);
        }
        
        return result;
    }

    public List<string> GetProperties()
    {
        var properties = type.GetProperties();
        var result = new List<string>();
        
        foreach (var prop in properties)
        {
            result.Add(prop.Name);
        }
        
        return result;
    }

    public bool HasAttribute<T>() where T : Attribute
    {
        return type.GetCustomAttribute<T>() != null;
    }
}
}
