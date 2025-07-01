using System;
using System.Reflection;
﻿using FileSystemCommands;

class task08
{
    static void Main()
    {
        string testFolder = @"C:\Users\vladi\OneDrive\Рабочий стол\ТЕСТ";
        string searchPattern = "*.*";

        var dll = Assembly.LoadFrom("FileSystemCommands.dll");

        var sizeCmd = (dynamic)dll.CreateInstance("FileSystemCommands.DirectorySizeCommand");
        sizeCmd.Execute(testFolder);
        Console.WriteLine($"Размер папки: {sizeCmd.Size}");

        var findCmd = (dynamic)dll.CreateInstance("FileSystemCommands.FindFilesCommand");
        findCmd.Execute(testFolder, searchPattern);
        
        Console.WriteLine("\nНайденные файлы:");
        foreach (var file in findCmd.FoundFiles)
            Console.WriteLine(Path.GetFileName(file));
    }
}
