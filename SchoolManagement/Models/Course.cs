using System.Security.AccessControl;
using System.IO;

namespace SchoolManagement;

public class Course
{
    public int Id;
    public string Name;
    public string Time;
    public Teacher Teacher;
    public int TeacherId;
    
    public void AssignTeacher(Teacher teacher)
    {
        Teacher = teacher;
        TeacherId = teacher.Id;
        Console.WriteLine($"Teacher {teacher.Name} assigned to {Name}");

    }


    
  
}
