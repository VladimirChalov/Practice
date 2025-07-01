using FileSystemCommands;
using Xunit;
using System.Reflection;

public class FileSystemCommandsTests
{
    [Fact]
    public void DirectorySizeCommand_ShouldCalculateSize()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "test1.txt"), "Hello");
        File.WriteAllText(Path.Combine(testDir, "test2.txt"), "World");

        var command = new DirectorySizeCommand(testDir);
        command.Execute(); // Проверяем, что не возникает исключений

        Directory.Delete(testDir, true);
    }

    [Fact]
    public void FindFilesCommand_ShouldFindMatchingFiles()
    {
        var testDir = Path.Combine(Path.GetTempPath(), "TestDir");
        Directory.CreateDirectory(testDir);
        File.WriteAllText(Path.Combine(testDir, "file1.txt"), "Text");
        File.WriteAllText(Path.Combine(testDir, "file2.log"), "Log");

        var command = new FindFilesCommand(testDir, "*.txt");
        command.Execute(); // Должен найти 1 файл

        Directory.Delete(testDir, true);
    }


    [Fact]
public void DirectorySizeCommand_ShouldReturnZeroForEmptyDirectory()
{
    var testDir = Path.Combine(Path.GetTempPath(), "EmptyTestDir");
    Directory.CreateDirectory(testDir); 

    var command = new DirectorySizeCommand(testDir);
    command.Execute();
    
    Assert.Equal(0, command.Size); 
    Directory.Delete(testDir);
}

    [Fact]
public void FindFilesCommand_ShouldHandleNonExistentDirectory()
{
    var nonExistentDir = Path.Combine(Path.GetTempPath(), "NonExistentFolder_" + Guid.NewGuid());
    var command = new FindFilesCommand(nonExistentDir, "*.txt");

    command.Execute();

    Assert.Empty(command.FoundFiles); 
}
}
