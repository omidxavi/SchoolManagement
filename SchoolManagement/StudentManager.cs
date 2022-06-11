using System.Net.Sockets;
using System.Security.Cryptography;
using SchoolManagement.DataLayer;

namespace SchoolManagement;

public class StudentManager
{
    private readonly List<Student> _students;
    
    public Student DefineNewStudent()
    {
        Console.WriteLine("Enter student name");
        var name = Console.ReadLine();
        Console.WriteLine("Enter student family");
        var family = Console.ReadLine();
        var student = new Student(name: name, family: family);

        Print(student);
        AddToList(student);
        var studentRepository = new StudentRepository();
        studentRepository.AddStudents(student);
        return student;
    }



    private void Print(Student student)
    {
        Console.WriteLine($"{student.Id} : {student.Name } + {student.Family} ");
    }

    /*public void PrintStudents()
    {
        var studentRepository = new StudentRepository();

        Console.WriteLine("-------Students--------------------------");
        foreach (var student in studentRepository.GetStudents())
        {
            Print(student);
            Console.WriteLine("**************************");
        }    
        Console.WriteLine("-----------------------------------------");
    }*/
    
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

    public Student SelectStudent()
    {
        var studentRepository = new StudentRepository();

        Console.WriteLine("select your student");
        for (int i = 0; i <studentRepository.GetStudents().Count ; i++)
        {
            var student = studentRepository.GetStudents()[i];
            Console.WriteLine($"{i+1} =>: {student.Name} , {student.Family}");
        }

        var input = Console.ReadLine();
        var selectIndex = int.Parse(input);
        var selectStudent = studentRepository.GetStudents()[selectIndex - 1];
        Console.WriteLine($"You selected {selectStudent.Family}");
        return selectStudent;
    }
}