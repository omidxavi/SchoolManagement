using System.Runtime.ExceptionServices;
using System.Text;



namespace SchoolManagement;

public class CourseManager
{

    private readonly List<Course> _courses;
    private readonly IdGeneratorCourse idGenerator;


    private const string CoursePath = "D:\\db\\Courses.csv";

    public CourseManager()
    {
        _courses =new CsvManager().GetCourses();
        idGenerator = new IdGeneratorCourse(_courses.Count);
    }



    public Course DefineNewCourse()
    {
        var id = idGenerator.GenerateId();
        Console.WriteLine("Enter course name");
        var name = Console.ReadLine();

        var course = new Course
        {
            Id = id,
            Name = name,
        };
        // Print(course);
        AddToList(course);
        AddToExcel(course);
        return course;
    }

    private void AddToExcel(Course course)
    {
        CsvManager csvManager = new CsvManager();
        csvManager.AddCourseToFile(course);
    }

    private void Print(Course course)
    {
        Console.WriteLine($"{course.Id} : {course.Name} ");
    }

    public void PrintCourses()
    {
        Console.WriteLine("----------------courses------------------------");
        foreach (var course in _courses)
        {
            Print(course);
            Console.WriteLine("*************************************");
        }

        Console.WriteLine("------------------------------------------------");
    }

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
    
    
    public void UpdateCourse()
    {
        var lines = File.ReadAllLines(CoursePath);
        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var columns = line.Split(",");
        }
        
    }

    
    public Course SelectCourse()
    {
        Console.WriteLine("Please select a course");
        for (int i = 0; i < _courses.Count; i++)
        {
            var course = _courses[i];
            Console.WriteLine($"{i + 1} -> {course.Name}");
        }

        var input = Console.ReadLine();
        var selectedIndex = int.Parse(input);
        var selectCourse = _courses[selectedIndex - 1];
        Console.WriteLine($"You selected {selectCourse.Name}");
        return selectCourse;
    }
}