using System;
using System.Reflection;
using System.IO;

class Program
{
    static void Main()
    {
        var dll = Assembly.LoadFrom("FileSystemCommands.dll");
        
        var sizeCmd = (dynamic)dll.CreateInstance("FileSystemCommands.DirectorySizeCommand");
        sizeCmd.Execute(@"C:\TestFolder");
        Console.WriteLine($"Размер: {sizeCmd.Size}");

        var findCmd = (dynamic)dll.CreateInstance("FileSystemCommands.FindFilesCommand");
        findCmd.Execute(@"C:\TestFolder", "*.*");
        
        foreach (var file in findCmd.FoundFiles)
            Console.WriteLine(Path.GetFileName(file));
    }
}
