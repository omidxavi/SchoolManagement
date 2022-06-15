using System.Configuration;
using System.Data.Odbc;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using Dapper;
using SchoolManagement.DataLayer;


namespace SchoolManagement;

public class CourseManager
{
    private const string ConnectionString =
        "Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=d:/db/SchoolManager.accdb";


    private readonly List<Course> _courses;

    public CourseManager()
    {
         _courses = new List<Course>();
    }
    
    private const string CoursePath = "D:\\db\\Courses.csv";
    
    
    public Course DefineNewCourse()
    {
        var courseRepository = new CourseRepository();

        Console.WriteLine("Enter course name");
        var name = Console.ReadLine();

        Course course = new Course();
        course.Name = name;
       
        Print(course);
        AddToList(course);
        
        try
        {
            var isExist = courseRepository.GetCourses().Exists(x => x.Name == name);

            if (!isExist)
            {
                courseRepository.AddCourses(course);
                Console.WriteLine("your course added...");
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        
        return course;
    }


    private void Print(Course course)
    {
        Console.WriteLine($"{course.Id} : {course.Name} ");
    }

    /*public void PrintCourses()
    {
        Console.WriteLine("----------------courses------------------------");
        foreach (var course in _courses)
        {
            Print(course);
            Console.WriteLine("*************************************");
        }

        Console.WriteLine("------------------------------------------------");
    }*/

    public void AddToList(Course course)
    {
        _courses.Add(course);
    }

    public void AssignTeacherToCourse(TeacherManager teacherManager)
    {

        var selectedCourse = SelectCourse();
        var selectedTeacher = teacherManager.selectedTeacher();
        selectedCourse.AssignTeacher(selectedTeacher);
        
        var csvManager = new CsvManager();
        csvManager.UpdateCourseTeacher(selectedCourse.Id, selectedTeacher.Id);
    }

    public void AssignTeachersToCourses(TeacherManager teacherManager)
    {
        var selectedCourse = SelectCourse();
        var selectedTeacher = teacherManager.selectedTeacher();
        var courseRepository = new CourseRepository();
        selectedCourse.TeacherId = selectedTeacher.Id;
        var course = new Course()
        {
            Name = selectedCourse.Name,
            TeacherId = selectedCourse.TeacherId
        };
        var isExistThisCourseTeacher = IsExistThisCourseTeacher(selectedTeacher.Id);
        if (isExistThisCourseTeacher) return;
        courseRepository.UpdateCourses(course);

    }
    
    public Course SelectCourse()
    {
        var courseRepository = new CourseRepository();

        Console.WriteLine("Please select a course");
        for (int i = 0; i <courseRepository.GetCourses().Count ; i++)
        {
            var course = courseRepository.GetCourses()[i];
            Console.WriteLine($"{i + 1} -> {course.Name}");
        }

        var input = Console.ReadLine();
        var selectedIndex = int.Parse(input);
        var selectCourse = courseRepository.GetCourses()[selectedIndex - 1];
        Console.WriteLine($"You selected {selectCourse.Name}");
        return selectCourse;
    }

    public bool IsExistThisCourseTeacher(int teacherId)
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.QueryFirstOrDefault<int>($"select Id from Course where TeacherId={teacherId}");
        if (result !=null && result>0)
        
            return true;
        return false;
    }
}