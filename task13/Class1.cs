using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization; 

namespace task13
{
public class Subject
{
  public string Name {get; set; }
  public int Grade {get; set; }
}

public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public List<Subject> Grades { get; set; }
}

public class Convert : JsonConverter<DateTime>
{
   string model = "yyyy-MM-dd";

   public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
{
    string dateString = reader.GetString();
    DateTime result = DateTime.ParseExact(dateString, model, null);
    return result;
}

  public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
{
    string dateString = value.ToString("yyyy-MM-dd");
    writer.WriteStringValue(dateString);
}
}
}
