using System.Security.Cryptography;
using System.Text;
using System.Threading.Channels;
using System.IO;
using SchoolManagement.DataLayer;


namespace SchoolManagement;

public class TeacherManager
{
    private readonly List<Teacher> _teachers;
    private readonly ITeacherRepository _teacherRepository;
    
    public TeacherManager(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
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
        try
        {
            var isExist =_teacherRepository != null && _teacherRepository.GetTeachers().Exists(x => x.Family == family);

            if (!isExist)
            {
                _teacherRepository.AddTeacher(teacher);
                Console.WriteLine("your course added...");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
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
        //var teacherRepository = new MsAccessTeacherRepository();
        for (int i = 0; i <_teacherRepository.GetTeachers().Count ; i++)
        {
            var teacher = _teacherRepository.GetTeachers()[i];
            Console.WriteLine($"{i+1} -> {teacher.Name} , {teacher.Family}");
        }

        var input = Console.ReadLine();
        var selectedIndex = int.Parse(input);
        var selectedTeacher = _teacherRepository.GetTeachers()[selectedIndex - 1];
        Console.WriteLine($"you selected {selectedTeacher.Name},{selectedTeacher.Family}");
        return selectedTeacher;
    }
}