// See https://aka.ms/new-console-template for more information

using System.Text;
using SchoolManagement;

var teacherManager = new TeacherManager();
var courseManager = new CourseManager();
var studentManager = new StudentManager();
var csvManager = new CsvManager();



var stop = false;

//Seed();
studentManager.PrintStudents();
teacherManager.PrintTeachers();
courseManager.PrintCourses();

while (!stop)
{

    if (!CheckCommand())
    {
        stop = true;
    }
      /*studentManager.PrintStudents();
    teacherManager.PrintTeachers();
    courseManager.PrintCourses();*/
}

void Seed()
{   
    SeedTeachers();
    SeedStudents();
    SeedCourses();

}

void SeedTeachers()
{
    var t1 = new Teacher()
    {
        Name = "Ostad A",
        Family = "A Family",
        Age = 50
    };
    teacherManager.AddToList(t1);    
    teacherManager.AddToList(new Teacher()
    {
        Name = "Ostad B",
        Family = "B Family",
        Age = 60
    });    
}

void SeedStudents()
{
    //studentManager.AddToList(new Student{Name = "Hamed", Family = "Nikzad"});
    //studentManager.AddToList(new Student{Name = "Omid", Family = "Moradi"});
}

void SeedCourses()
{
    courseManager.AddToList(new Course() {Name = "Physic", Time ="100"} );
    courseManager.AddToList(new Course() {Name = "Math",Time = "500"});
}




bool CheckCommand()
{
    Console.WriteLine("operations:");
    Console.WriteLine("1 -> Define Teacher");
    Console.WriteLine("2 -> Define Course");
    Console.WriteLine("3 -> Define Student");
    Console.WriteLine("4 -> Assign teacher to course");
    Console.WriteLine("5 -> Assign course to student");
    Console.WriteLine("q -> Quit");
    var input = Console.ReadLine();

    switch (input.ToLower())
    {
        case "1":
            teacherManager.DefineNewTeacher();
            teacherManager.PrintTeachers();
            break;
        case "2":
            courseManager.DefineNewCourse();
            courseManager.PrintCourses();
            break;
            
        case "3":
            studentManager.DefineNewStudent();
            studentManager.PrintStudents();
            break;
            
        case "4":
            courseManager.AssignTeacherToCourse(teacherManager);
            break;
        case "5":
            studentManager.AssignCourseToStudent(courseManager);
            break;

        case "q":
            Console.WriteLine("Exit");

            return false;
            break;
        default:
            Console.WriteLine("you should Enter valid number...");
            
            break;
    }

    return true;
}