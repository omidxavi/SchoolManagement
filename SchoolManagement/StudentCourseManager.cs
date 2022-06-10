using SchoolManagement.DataLayer;

namespace SchoolManagement;

public class StudentCourseManager
{
    public void AssignCourseToStudent(CourseManager courseManager , StudentManager studentManager)
    {
        var selectedStudent = studentManager.SelectStudent();
        var selectedCourse = courseManager.SelectCourse();
        var studentCourse = new StudentCourse();
        studentCourse.StudentId = selectedStudent.Id;
        studentCourse.CourseId = selectedCourse.Id;
        var studentCourseRepository = new StudentCourseRepository();
        studentCourseRepository.AddStudentCourses(studentCourse);
        
    }
}