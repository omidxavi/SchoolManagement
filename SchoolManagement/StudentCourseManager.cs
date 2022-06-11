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
        studentCourseRepository.AddStudentCourses(studentCourse);
    }
}