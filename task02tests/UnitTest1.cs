using Xunit;
using System.Linq;
using task02;

namespace task02tests;

public class StudentServiceTests
{
    private List<Student> _testStudents;
    private StudentService _service;

    public void StudentServiceTests()
    {
        _testStudents = new List<Student>
        {
            new() { Name = "Иван", Faculty = "ФИТ", Grades = new List<int> { 5, 4, 5 } },
            new() { Name = "Анна", Faculty = "ФИТ", Grades = new List<int> { 3, 4, 3 } },
            new() { Name = "Петр", Faculty = "Экономика", Grades = new List<int> { 5, 5, 5 } }
        };
        _service = new StudentService(_testStudents);
    }

    [Fact]
    public void GetStudentsByFaculty_ReturnsCorrectStudents()
    {
        var result = _service.GetStudentsByFaculty("ФИТ").ToList();
        Assert.AreEqual(2, result.Count);
        Assert.IsTrue(result.All(s => s.Faculty == "ФИТ"));
    }

    [Fact]
    public void GetFacultyWithHighestAverageGrade_ReturnsCorrectFaculty()
    {
        var result = _service.GetFacultyWithHighestAverageGrade();
        Assert.AreEqual("Экономика", result);
    }
}
[Fact]
public void GetStudentsWithMinAverageGrade_ReturnsCorrectStudents()
{
    var result = _service.GetStudentsWithMinAverageGrade(3.33).ToList();
    Assert.Single(result);
    Assert.Equal("Анна", result[0].Name);
}

[Fact]
public void GetStudentsOrderedByName_ReturnsCorrectOrder()
{
    var result = _service.GetStudentsOrderedByName().ToList();
    Assert.Equal("Анна", result[0].Name);
    Assert.Equal("Иван", result[1].Name);
    Assert.Equal("Мария", result[2].Name);
    Assert.Equal("Петр", result[3].Name);
}

[Fact]
public void GroupStudentsByFaculty_ReturnsCorrectGroups()
{
    var result = _service.GroupStudentsByFaculty();
    Assert.Equal(2, result.Count);

    var fitStudents = result["ФИТ"].ToList();
    Assert.Equal(2, fitStudents.Count);

    var economyStudents = result["Экономика"].ToList();
    Assert.Equal(2, economyStudents.Count);
}

[Fact]
public void GetStudentsWithMinAverageGrade_WhenNoMatches_ReturnsEmpty()
{
    var result = _service.GetStudentsWithMinAverageGrade(5.0).ToList();
    Assert.Empty(result);
}

[Fact]
public void GetFacultyWithHighestAverageGrade_WithEmptyList_ReturnsNull()
{
    var emptyService = new StudentService(new List<Student>());
    var result = emptyService.GetFacultyWithHighestAverageGrade();
    Assert.Null(result);
}
