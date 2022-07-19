using System.Configuration;
using System.Data.Odbc;
using System.Diagnostics;
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
    private readonly ICourseRepository _courseRepository;

    public CourseManager(ICourseRepository courseRepository)
    {
         _courses = new List<Course>();
         _courseRepository = courseRepository;
    }
    
    private const string CoursePath = "D:\\db\\Courses.csv";
    
    
    public Course DefineNewCourse()
    {
        var courseRepository = new MsAccessCourseRepository();
        var courseRepositoryPostgres = new PostgresCourseRepository();

        Console.WriteLine("Enter course name");
        var name = Console.ReadLine();

        Course course = new Course();
        course.Name = name;
       
        Print(course);
        AddToList(course);
        
        try
        { 
            bool isExist = courseRepository.GetCourses().Exists(x => x.Name == name);

            if (!isExist)
            {
                courseRepository.AddCourses(course);
                Console.WriteLine("your course added...");
                courseRepositoryPostgres.AddCourses(course);
            }

            Console.WriteLine("you have this course");

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
        //var courseRepository = new MsAccessCourseRepository();
        //var courseRepositoryPostgres = new PostgresCourseRepository();
        selectedCourse.TeacherId = selectedTeacher.Id;
        var course = new Course()
        {
            Name = selectedCourse.Name,
            TeacherId = selectedCourse.TeacherId
        };
        var isExistThisCourseTeacher = IsExistThisCourseTeacher(selectedTeacher.Id,selectedCourse.Name);
        if (isExistThisCourseTeacher) return;
        _courseRepository.UpdateCourses(course);
        //_courseRepository.UpdateCourses(course);
    }
    
    public Course SelectCourse()
    {
        //var courseRepository = new MsAccessCourseRepository();

        Console.WriteLine("Please select a course");
        var result = _courseRepository.GetCourses();
        Console.WriteLine(result);

        if (result!= null)
            for (int i = 0; i < result.Count; i++)
            {
                var course = _courseRepository.GetCourses()[i];
                Console.WriteLine($"{i + 1} -> {course.Name}");
            }
        
        

        var input = Console.ReadLine();
        var selectedIndex = int.Parse(input);
        var selectCourse = _courseRepository.GetCourses()[selectedIndex - 1];
        Console.WriteLine($"You selected {selectCourse.Name}");
        return selectCourse;
    }

    public bool IsExistThisCourseTeacher(int teacherId,string name)
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.QueryFirstOrDefault<int>($"select Id from Course where TeacherId={teacherId} and NAME ='{name}'");
        if (result !=null && result>0)
        
            return true;
        return false;
    }
}