using SchoolManagement.DataLayer;

namespace SchoolManagement;

public class StudentCourseManager
{
    public void AssignCourseToStudent( StudentManager studentManager ,CourseManager courseManager)
    {
        var selectedStudent = studentManager.SelectStudent();
        var selectedCourse = courseManager.SelectCourse();
        var studentCourse = new StudentCourse
        {
            StudentId = selectedStudent.Id,
            CourseId = selectedCourse.Id
        };
        var studentCourseRepository = new StudentCourseRepository();

        try
        {
            /*var isExist= studentCourseRepository.GetStudentCourses().Exists(x => x.StudentId==selectedStudent.Id && );

            if (!isExist)
            {
                // roomRepository.AddRooms(room);
                Console.WriteLine("your course added...");
            }*/
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }        
 

    }
//studentCourseRepository.AddStudentCourses(studentCourse);