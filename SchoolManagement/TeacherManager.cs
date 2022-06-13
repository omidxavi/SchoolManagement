using System.Security.Cryptography;
using System.Text;
using System.Threading.Channels;
using System.IO;
using SchoolManagement.DataLayer;


namespace SchoolManagement;

public class TeacherManager
{
    private readonly List<Teacher> _teachers;

    public TeacherManager()
    {
        _teachers = new List<Teacher>();
    }
    
    
    public Teacher DefineNewTeacher()
    {
        
        Console.WriteLine("Enter teacher name");
        var name = Console.ReadLine();
        Console.WriteLine("Enter teacher family");
        var family = Console.ReadLine();
        var teacher = new Teacher
        {
            Name = name,
            Family = family,
        };
        print(teacher);
        AddToList(teacher);
        var teacherRepository = new TeacherRepository();
        try
        {
            var isExist = teacherRepository.GetTeachers().Exists(x => x.Family == family);

            if (!isExist)
            {
                teacherRepository.AddTeacher(teacher);
                Console.WriteLine("your course added...");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return teacher;
    }
    


 
    private void print(Teacher teacher)
    {
        Console.WriteLine($"{teacher.Id} : {teacher.Name} + {teacher.Family} ");
    }

    /*public void PrintTeachers()
    {
        Console.WriteLine("-----------------------Teachers------------------------");
        foreach (var teacher in _teachers )
        {
            print(teacher);
            Console.WriteLine("*******************************************");
        }
    }*/
     

    public void AddToList(Teacher teacher)
    {
        _teachers.Add(teacher);
    }
    public Teacher selectedTeacher()
    {
        Console.WriteLine("please select a teacher");
        var teacherRepository = new TeacherRepository();
        for (int i = 0; i <teacherRepository.GetTeachers().Count ; i++)
        {
            var teacher = teacherRepository.GetTeachers()[i];
            Console.WriteLine($"{i+1} -> {teacher.Name} , {teacher.Family}");
        }

        var input = Console.ReadLine();
        var selectedIndex = int.Parse(input);
        var selectedTeacher = teacherRepository.GetTeachers()[selectedIndex - 1];
        Console.WriteLine($"you selected {selectedTeacher.Name},{selectedTeacher.Family}");
        return selectedTeacher;
    }
}