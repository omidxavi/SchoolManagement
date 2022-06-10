using System.Runtime.InteropServices;

namespace SchoolManagement;

public class Student
{
    public int Id;
    public string Name;
    public string Family;
    public List<Course> Courses;


    public Student(string name, string family)
    {
        Name = name;
        Family = family;
        Courses = new List<Course>();
    }

    public void AssignCourse(Course course)
    {
        Courses.Add(course);
        Console.WriteLine($" course {course.Name} assigned to {Name} , {Family}");
    }

    public void AssignCourses(List<Course> courses)
    {
        Courses = courses;
    }
}