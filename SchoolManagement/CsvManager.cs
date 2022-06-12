using System.Data.Common;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace SchoolManagement;

public class CsvManager
{
    private const string TeacherPath = "D:\\db\\Teachers.csv";
    private const string StudentPath = "D:\\db\\student.csv";
    private const string StudentCoursePath = "D:\\db\\student-course.csv";
    private const string CourseTeacherPath = "D:\\db\\course-teacher.csv";
    private const string CoursePath = "D:\\db\\Courses.csv";
    private const string RoomPath = "D:\\db\\Rooms.csv";
    private const string RoomCalenderPath = "D:\\db\\Roomscalender.csv";


    public List<Teacher> GetTeachers()
    {
        var teachers = new List<Teacher>();
        if (!File.Exists(TeacherPath))
        {
            return teachers;
        }

        var lines = File.ReadAllLines(TeacherPath);
        for (int i = 0; i < lines.Length; i++)
        {
            if (i == 0)
                continue;
            var line = lines[i];
            var columns = line.Split(',');
            if (columns.Length != 3)
                continue;
            var teacher = new Teacher()
            {
                //Id = int.Parse(columns[0]),
                Name = columns[1],
                Family = columns[2]
            };
            teachers.Add(teacher);
        }

        return teachers;
    }

    public void AddTeacherTOFile(Teacher teacher)
    {
        if (!File.Exists(TeacherPath))
        {
            var csv = new StringBuilder();
            csv.AppendLine("ID,name,family");
            var first = teacher.Id.ToString();
            var second = teacher.Name.ToString();
            var third = teacher.Family.ToString();
            var row = $"{first},{second},{third}";
            csv.AppendLine(row);
            File.AppendAllText(TeacherPath, csv.ToString());
        }
        else
        {
            var csv = new StringBuilder();
            var first = teacher.Id.ToString();
            var second = teacher.Name.ToString();
            var third = teacher.Family.ToString();
            var row = $"{first},{second},{third}";
            csv.AppendLine(row);
            File.AppendAllText(TeacherPath, csv.ToString());
        }
    }

    public Teacher GetTeacherById(int teacherId)
    {
        var teachers = GetTeachers();
        foreach (var teacher in teachers)
        {
            if (teacher.Id == teacherId)
                return teacher;
        }

        return null;
    }

    public List<Course> GetCourses()
    {
        var Courses = new List<Course>();
        if (!File.Exists(CoursePath))
        {
            return Courses;
        }


        var lines = File.ReadAllLines(CoursePath);
        for (int i = 0; i < lines.Length; i++)
        {
            if (i == 0)
            {
                continue;
            }

            var line = lines[i];
            var columns = line.Split(",");
            if (columns.Length != 3)
            {
                continue;
            }

            var course = new Course()
            {
                Id = Int32.Parse(columns[0]),
                Name = columns[1]
            };
            course.Teacher = GetTeacherById(course.TeacherId);
            Courses.Add(course);
        }

        return Courses;
    }

    public void AddCourseToFile(Course course)
    {
        if (!File.Exists(CoursePath))
        {
            var csv = new StringBuilder();
            csv.AppendLine("ID,Name,teacherId");
            var first = course.Id;
            var second = course.Name;
            var third = course.TeacherId;
            var row = $"{first},{second},{third}";
            csv.AppendLine(row);
            File.AppendAllText(CoursePath, csv.ToString());
        }
        else
        {
            var csv = new StringBuilder();
            var first = course.Id;
            var second = course.Name;
            var third = course.TeacherId;
            var row = $"{first},{second},{third}";
            csv.AppendLine(row);
            File.AppendAllText(CoursePath, csv.ToString());
        }
    }

    public List<Student> GetStudents()
    {
        var Students = new List<Student>();

        if (!File.Exists(StudentPath))
        {
            return Students;
        }

        var allStudentCourses = GetStudentCourses();
        var allCourses = GetCourses();

        var lines = File.ReadAllLines(StudentPath);
        for (int i = 0; i < lines.Length; i++)
        {
            if (i == 0)
            {
                continue;
            }

            var line = lines[i];
            var columns = line.Split(",");
            if (columns.Length != 3)
            {
                continue;
            }

            var student = new Student()
            {
                Name = columns[0],
                Family = columns[1]
            };
            var courseIds = allStudentCourses.Where(sc => sc.StudentId == student.Id).Select(sc => sc.CourseId)
                .ToList();
            var studentCourses = allCourses.Where(c => courseIds.Contains(c.Id)).ToList();
            student.AssignCourses(studentCourses);

            Students.Add(student);
        }

        return Students;
    }

    public void AddStudentToFile(Student student)
    {
        if (!File.Exists(StudentPath))
        {
            var csv = new StringBuilder();
            csv.AppendLine("ID,name,family");
            var first = student.Id;
            var second = student.Name;
            var third = student.Family;
            var row = $"{first},{second},{third}";
            csv.AppendLine(row);
            File.AppendAllText(StudentPath, csv.ToString());
        }
        else
        {
            var csv = new StringBuilder();
            var first = student.Id;
            var second = student.Name;
            var third = student.Family;
            var row = $"{first},{second},{third}";
            csv.AppendLine(row);
            File.AppendAllText(StudentPath, csv.ToString());
        }
    }

    private List<StudentCourse> GetStudentCourses()
    {
        var studentCourses = new List<StudentCourse>();
        if (!File.Exists(StudentCoursePath))
        {
            return studentCourses;
        }

        var lines = File.ReadAllLines(StudentCoursePath);
        for (int i = 0; i < lines.Length; i++)
        {
            if (i == 0)
            {
                continue;
            }

            var line = lines[i];
            var columns = line.Split(",");
            if (columns.Length != 3)
            {
                continue;
            }

            var studentId = int.Parse(columns[1]);
            var courseId = int.Parse(columns[2]);
            var studentCourse = new StudentCourse()
            {
                CourseId = courseId,
                StudentId = studentId
            };
            studentCourses.Add(studentCourse);
        }

        return studentCourses;
    }

    public void AddToStudentCourseFile(Student student, Course course)
    {
        var count = GetStudentCourses();
        var idGenerator = new IdGeneratorStudentCourse(count.Count);

        if (!File.Exists(StudentCoursePath))
        {
            var csv = new StringBuilder();
            csv.AppendLine("ID,studentId,courseId");
            var zero = idGenerator.GenerateId();
            var first = student.Id;
            var second = course.Id;
            var row = $"{zero},{first},{second}";
            csv.AppendLine(row);
            File.AppendAllText(StudentCoursePath, csv.ToString());
        }
        else
        {
            var csv = new StringBuilder();
            var zero = idGenerator.GenerateId();
            var first = student.Id;
            var second = course.Id;
            var row = $"{zero},{first},{second}";
            csv.AppendLine(row);
            File.AppendAllText(StudentCoursePath, csv.ToString());
        }
    }

    public void UpdateCourseTeacher(int courseId, int teacherId)
    {
        var lines = File.ReadAllLines(CoursePath);
        for (int i = 1; i < lines.Length; i++)
        {
            var line = lines[i];
            var columns = line.Split(",");
            if (int.Parse(columns[0]) == courseId)
            {
                lines[i] = $"{columns[0]},{columns[1]},{teacherId}";
                break;
            }
        }

        File.WriteAllLines(CoursePath, lines);
    }

    public void AddToRoomsFile(Room room)
    {
        if (!File.Exists(RoomPath))
        {
            var csv = new StringBuilder();
            csv.AppendLine("Id,Number");
            var first = room.Id;
            var second = room.Number;
            var row = $"{first},{second}";
            csv.AppendLine(row);
            File.AppendAllText(RoomPath, csv.ToString());
        }
        else
        {
            var csv = new StringBuilder();
            var first = room.Id;
            var second = room.Number;
            var row = $"{first},{second}";
            csv.AppendLine(row);
            File.AppendAllText(RoomPath, csv.ToString());
        }
    }
    
    public List<Room> GetRoom()
    {
        var rooms = new List<Room>();
        if (!File.Exists(RoomPath))
        {
            return rooms;
        }

        var lines = File.ReadAllLines(RoomPath);
        for (int i = 0; i < lines.Length; i++)
        {
            if (i == 0)
            {
                continue;
            }

            var line = lines[i];
            var columns = line.Split(",");
            var room = new Room(int.Parse(columns[1]));
            rooms.Add(room);
        }

        return rooms;
    }

    public void AddToRoomCalenderFile(RoomsCalender roomsCalender)
    {
        if (!File.Exists(RoomCalenderPath))
        {
            var csv = new StringBuilder();
            csv.AppendLine("Id,RoomId,CourseID,Day,Time");
            var first = roomsCalender.Id;
            var second = roomsCalender.RoomId;
            var third = roomsCalender.CourseId;
            var fourth = roomsCalender.Day;
            var fifth = roomsCalender.Time;
            var row = $"{first},{second},{third},{fourth},{fifth}";
            csv.AppendLine(row);
            File.AppendAllText(RoomCalenderPath, csv.ToString());
        }
        else
        {
            var csv = new StringBuilder();
            var first = roomsCalender.Id;
            var second = roomsCalender.RoomId;
            var third = roomsCalender.CourseId;
            var fourth = roomsCalender.Day;
            var fifth = roomsCalender.Time;
            var row = $"{first},{second},{third},{fourth},{fifth}";
            csv.AppendLine(row);
            File.AppendAllText(RoomCalenderPath, csv.ToString());
        }
    }

    public List<RoomsCalender> GetRoomsCalender()
    {
        var roomsCalenders = new List<RoomsCalender>();
        if (!File.Exists(RoomCalenderPath))
        {
            return roomsCalenders;
        }

        var lines = File.ReadAllLines(RoomCalenderPath);
        for (int i = 0; i < lines.Length; i++)
        {
            if (i == 0)
            {
                continue;
            }

            var line = lines[i];
            var columns = line.Split(",");
            var roomsCalender = new RoomsCalender();
            // Enum changed to int but in file we have string can not read...
            roomsCalenders.Add(roomsCalender);
        }

        return roomsCalenders;
    }
    
    public List<CourseTeacher> GetCourseTeacher()
    {
        var courseTeachers = new List<CourseTeacher>();
        if (!File.Exists(CourseTeacherPath))
        {
            return courseTeachers;
        }

        var lines = File.ReadAllLines(CourseTeacherPath);
        for (int i = 0; i < lines.Length; i++)
        {
            if (i == 0)
            {
                continue;
            }

            var line = lines[i];
            var columns = line.Split(",");
            if (columns.Length != 3)
            {
                continue;
            }

            var courseId = int.Parse(columns[1]);
            var teacherId = int.Parse(columns[2]);
            var courseTeacher = new CourseTeacher()
            {
                CourseId = courseId,
                TeacherId = teacherId
            };
            courseTeachers.Add(courseTeacher);
        }

        return courseTeachers;
    }

    public void AddToCourseTeacherFile(Course course, Teacher teacher)
    {
        if (!File.Exists(CourseTeacherPath))
        {
            Random random = new Random();
            var csv = new StringBuilder();
            csv.AppendLine("Id,courseId,teacherId");
            var zero = random.Next(1000);
            var first = course.Id;
            var second = teacher.Id;
            var row = $"{zero},{first},{second}";
            csv.AppendLine(row);
            File.AppendAllText(CourseTeacherPath, csv.ToString());
        }
        else
        {
            Random random = new Random();
            var csv = new StringBuilder();
            var zero = random.Next(1000);
            var first = course.Id;
            var second = teacher.Id;
            var row = $"{zero},{first},{second}";
            csv.AppendLine(row);
            File.AppendAllText(CourseTeacherPath, csv.ToString());
        }
    }

    public bool CheckCourseTeacherId(Course course, Teacher teacher)
    {
        var allTeacher = GetTeachers();
        var allCourse = GetCourses();
        for (int i = 0; i < allCourse.Count; i++)
        {
            if ((course.Id != allCourse[i].Id) && (teacher.Id != allTeacher[i].Id))
            {
                return true;
            }
        }

        return false;
    }

    public void UpdateCourse(Course course)
    {
        var lines = File.ReadAllLines(CoursePath);
        for (int i = 1; i < lines.Length; i++)
        {
            var line = lines[i];
            var columns = line.Split(",");
            if (int.Parse(columns[0]) == course.Id)
            {
                lines[i] = $"{columns[0]},{course.Name},{course.TeacherId}";
                break;
            }
        }

        File.WriteAllLines(CoursePath, lines);
    }
}