using CommandLib;
using System.IO;

namespace FileSystemCommands;

public class DirectorySizeCommand : ICommand
{
    public string DirectoryPath {get;}
    public long Size {get; set;}

    public DirectorySizeCommand(string input)
    {
        DirectoryPath = input;
        Size = 0;
    }

    public void Execute()
    {
        Size = CalculateDirectorySize(DirectoryPath);
        Console.WriteLine($"Directory size: {Size} bytes");
    }

    private long CalculateDirectorySize(string path)
    {
        if (!Directory.Exists(path))
            throw new DirectoryNotFoundException(path);

        return Directory.GetFiles(path, "*", SearchOption.AllDirectories)
                       .Sum(file => new FileInfo(file).Length);
    }
}

public class FindFilesCommand : ICommand
{
    public string DirectoryPath {get;}
    public string SearchPattern {get;}
    public string[] FoundFiles {get; private set;}

    public FindFilesCommand(string directoryPath, string searchPattern)
    {
        DirectoryPath = directoryPath;
        SearchPattern = searchPattern;
        FoundFiles = Array.Empty<string>();
    }

    public void Execute()
    {
        if (Directory.Exists(DirectoryPath))
        {
            FoundFiles = Directory.GetFiles(DirectoryPath, SearchPattern, SearchOption.AllDirectories);
            Console.WriteLine($"Found {FoundFiles.Length} files:");
            foreach (var file in FoundFiles)
                Console.WriteLine(file);
        }
        else
        {
            Console.WriteLine("Directory not found");
            FoundFiles = Array.Empty<string>();
        }
    }
}
