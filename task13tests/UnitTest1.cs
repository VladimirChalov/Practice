using task13;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Xunit;

namespace task13tests;

class task13tests{
    
[Fact]
public void Serialize_Student_Works()
{
    var student = new Student
    {
        FirstName = "Анна",
        Grades = new List<Subject> { new Subject { Name = "Литература" } }
    };

    string json = JsonSerializer.Serialize(student);
    
    if (string.IsNullOrEmpty(json)) 
        throw new Exception("Не работае");
}

[Fact]
public void Deserialize_Student_Works()
{
    string json = @"{""FirstName"":""Иван""}";
    var student = JsonSerializer.Deserialize<Student>(json);
    
    if (student?.FirstName != "Иван")
        throw new Exception("Не работае");
}
}
