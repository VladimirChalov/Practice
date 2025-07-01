using CommandLib;
using System.IO;

namespace FileSystemCommands
{
    public class DirectorySizeCommand : ICommand
    {
        public string DirectoryPath {get;}
        public int Size {get; set;}

        public DirectorySizeCommand(string input)
        {
            DirectoryPath = input;
            Size = 0;
        }

        public void Execute()
        {
            Size = 0;
            if (Directory.Exists(DirectoryPath))
            {
                foreach (var file in Directory.GetFiles(DirectoryPath, "*", SearchOption.AllDirectories))
                {
                    Size += new FileInfo(file).Length;
                }
            }
        }
    }
    public class FindFilesCommand : ICommand
    {
        public string DirectoryPath {get;}
        public string SearchPattern {get;}
        public string[] FoundFiles {get; set;}

        public FindFilesCommand(string input_directory_path, string input_search_pattern)
        {
            DirectoryPath = input_directory_path;
            SearchPattern = input_search_pattern;
            FoundFiles = Array.Empty<string>();
        }
    public void Execute()
{
    if (Directory.Exists(DirectoryPath))
    {
        FoundFiles = Directory.GetFiles(DirectoryPath, SearchPattern, SearchOption.AllDirectories);
    }
    else
    {
        FoundFiles = Array.Empty<string>();
    }
}
}
}
