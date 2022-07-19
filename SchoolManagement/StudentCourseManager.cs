using SchoolManagement.DataLayer;

namespace SchoolManagement;

public class StudentCourseManager
{
    private readonly IStudentCourseRepository _studentCourseRepository;

    public StudentCourseManager(IStudentCourseRepository studentCourseRepository)
    {
        _studentCourseRepository = studentCourseRepository;
    }
    public void AssignCourseToStudent( StudentManager studentManager ,CourseManager courseManager)
    {
        var selectedStudent = studentManager.SelectStudent();
        var selectedCourse = courseManager.SelectCourse();
        var studentCourse = new StudentCourse
        {
            StudentId = selectedStudent.Id,
            CourseId = selectedCourse.Id
        };
        //var studentCourseRepository = new MsAccessStudentCourseRepository();
        //var studentCourseRepositoryPostgres = new PostgresStudentCourseRepository();
        _studentCourseRepository.SetStudentCourse(studentCourse.StudentId,studentCourse.CourseId);
        //studentCourseRepositoryPostgres.SetStudentCourse(studentCourse.StudentId,studentCourse.CourseId);
    }        
 

}
