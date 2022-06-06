using System.Security.Cryptography;
using System.Text;
using System.Threading.Channels;
using System.IO;


namespace SchoolManagement;

public class TeacherManager
{
    private readonly List<Teacher> _teachers;
    private readonly IdGeneratorTeacher idGenerator;

    public TeacherManager()
    {
        _teachers = new CsvManager().GetTeachers();
        idGenerator = new IdGeneratorTeacher(_teachers.Count);
    }

    

    public Teacher DefineNewTeacher()
    {
        
        var id = idGenerator.GenerateId();
        Console.WriteLine("Enter teacher name");
        var name = Console.ReadLine();
        Console.WriteLine("Enter teacher family");
        var family = Console.ReadLine();
        var teacher = new Teacher
        {
            Id = id,
            Name = name,
            Family = family,
        };
        print(teacher);
        AddToList(teacher);
        AddTOExcel(teacher);
        
        return teacher;
    }
    

    private void AddTOExcel(Teacher teacher)
    {
        CsvManager csvManager = new CsvManager();
        csvManager.AddTeacherTOFile(teacher);
    }

 
    private void print(Teacher teacher)
    {
        Console.WriteLine($"{teacher.Id} : {teacher.Name} + {teacher.Family}  and age is : {teacher.Age}");
    }

    public void PrintTeachers()
    {
        Console.WriteLine("-----------------------Teachers------------------------");
        foreach (var teacher in _teachers )
        {
            print(teacher);
            Console.WriteLine("*******************************************");
        }
    }
     

    public void AddToList(Teacher teacher)
    {
        _teachers.Add(teacher);
        
    }
    public Teacher selectedTeacher()
    {
        Console.WriteLine("please select a teacher");
        for (int i = 0; i < _teachers.Count; i++)
        {
            var teacher = _teachers[i];
            Console.WriteLine($"{i+1} -> {teacher.Name} , {teacher.Family}");
        }

        var input = Console.ReadLine();
        var selectedIndex = int.Parse(input);
        var selectedTeacher = _teachers[selectedIndex - 1];
        Console.WriteLine($"you selected {selectedTeacher.Name},{selectedTeacher.Family}");
        return selectedTeacher;
    }
}