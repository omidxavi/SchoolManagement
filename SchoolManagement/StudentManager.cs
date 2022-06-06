using System.Net.Sockets;
using System.Security.Cryptography;

namespace SchoolManagement;

public class StudentManager
{
    private readonly List<Student> _students;
    private readonly IdGeneratorStudent idGenerator;
    private readonly IdGeneratorStudentCourse _idGeneratorStudentCourse;


    public StudentManager()
    {
        _students = new CsvManager().GetStudents();
        idGenerator = new IdGeneratorStudent(_students.Count);
    }



    
    public Student DefineNewStudent()
    {
        var id = idGenerator.GenerateId();
        Console.WriteLine("Enter student name");
        var name = Console.ReadLine();
        Console.WriteLine("Enter student family");
        var family = Console.ReadLine();

        var student = new Student(id: id, name: name, family: family);
     

        Print(student);
        AddToList(student);
        AddToExcel(student);
        
        return student;
    }

    private void AddToExcel(Student student)
    {
        CsvManager csvManager = new CsvManager();
        csvManager.AddStudentToFile(student);
    }

    private void Print(Student student)
    {
        Console.WriteLine($"{student.Id} : {student.Name } + {student.Family} ");
    }

    public void PrintStudents()
    {
        Console.WriteLine("-------Students--------------------------");
        foreach (var student in _students)
        {
            Print(student);
            Console.WriteLine("**************************");
        }    
        Console.WriteLine("-----------------------------------------");
    }
    
    public void AddToList(Student student)
    {
        _students.Add(student);
    }

    public void AssignCourseToStudent(CourseManager courseManager)
    {
        var selectedStudent = SelectStudent();
        var selectedCourse = courseManager.SelectCourse();
        selectedStudent.AssignCourse(selectedCourse);
        var csvManager = new CsvManager();
        csvManager.AddToStudentCourseFile(selectedStudent,selectedCourse);
    }

    private Student SelectStudent()
    {
        Console.WriteLine("select your student");
        for (int i = 0; i < _students.Count; i++)
        {
            var student = _students[i];
            Console.WriteLine($"{i+1} -> {student.Name} , {student.Family}");

        }

        var input = Console.ReadLine();
        var selectIndex = int.Parse(input);
        var selectStudent = _students[selectIndex - 1];
        return selectStudent;
    }
}