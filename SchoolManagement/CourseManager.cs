using System.Runtime.ExceptionServices;
using System.Text;
using SchoolManagement.DataLayer;


namespace SchoolManagement;

public class CourseManager
{

    private readonly List<Course> _courses;
    
    private const string CoursePath = "D:\\db\\Courses.csv";
    
    
    public Course DefineNewCourse()
    {
        Console.WriteLine("Enter course name");
        var name = Console.ReadLine();

        var course = new Course
        {
            Name = name,
        };
        Print(course);
        AddToList(course);
        var courseRepository = new CourseRepository();
        courseRepository.AddCourses(course);
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
        var selectCourse = _courses[selectedIndex - 1];
        Console.WriteLine($"You selected {selectCourse.Name}");
        return selectCourse;
    }
}