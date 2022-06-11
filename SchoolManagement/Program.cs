// See https://aka.ms/new-console-template for more information

using System.Text;
using SchoolManagement;
//using SchoolManagement.DataLayer;

//var teachers = new TeacherRepository().GetTeachers();
var teacherManager = new TeacherManager();
var courseManager = new CourseManager();
var studentManager = new StudentManager();
var roomManager = new RoomManager();
var roomCalenderManager = new RoomCalenderManager();
var studentCourseManager = new StudentCourseManager();
var csvManager = new CsvManager();



var stop = false;

//Seed();
//studentManager.PrintStudents();
teacherManager.PrintTeachers();
//courseManager.PrintCourses();

while (!stop)
{

    if (!CheckCommand())
    {
        stop = true;
    }

    bool CheckCommand()
    {
        Console.WriteLine("operations:");
        Console.WriteLine("1 -> Define Teacher");
        Console.WriteLine("2 -> Define Course");
        Console.WriteLine("3 -> Define Student");
        Console.WriteLine("4 -> Define Room");
        Console.WriteLine("5 -> Assign teacher to course");
        Console.WriteLine("6 -> Assign course to student");
        Console.WriteLine("7 -> CalenderYour Course and Room and Date");
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
                //courseManager.PrintCourses();
                break;

            case "3":
                studentManager.DefineNewStudent();
                //studentManager.PrintStudents();
                break;
            case "4":
                roomManager.GetRoomFromUser();
                roomManager.PrintRooms();
                break;

            case "5":
                courseManager.AssignTeacherToCourse(teacherManager);
                break;
            case "6":
                studentCourseManager.AssignCourseToStudent(studentManager,courseManager);

                break;
            case "7":
                roomCalenderManager.DefineGeneralCalender(roomManager, courseManager);
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
}