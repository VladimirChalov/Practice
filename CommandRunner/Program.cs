using System;
using System.Reflection;
using System.IO;

class Program
{
    static void Main()
    {
        string testFolder = @"C:\Users\vladi\OneDrive\Рабочий стол\ТЕСТ";
        string searchPattern = "*.*";

        try
        {
            if (!File.Exists("FileSystemCommands.dll"))
            {
                Console.WriteLine("Ошибка: FileSystemCommands.dll не найдена");
                return;
            }

            var dll = Assembly.LoadFrom("FileSystemCommands.dll");

            dynamic sizeCmd = dll.CreateInstance("FileSystemCommands.DirectorySizeCommand");
            sizeCmd.Execute(testFolder);
            Console.WriteLine($"Размер папки: {sizeCmd.Size}");

            dynamic findCmd = dll.CreateInstance("FileSystemCommands.FindFilesCommand");
            findCmd.Execute(testFolder, searchPattern);

            Console.WriteLine("\nНайденные файлы:");
            foreach (var file in findCmd.FoundFiles)
                Console.WriteLine(Path.GetFileName(file));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
    }
}
