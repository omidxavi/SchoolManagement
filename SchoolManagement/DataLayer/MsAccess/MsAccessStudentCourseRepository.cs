using System.Data.Odbc;
using Dapper;
namespace SchoolManagement.DataLayer;

public class MsAccessStudentCourseRepository:IStudentCourseRepository
{
    private const string ConnectionString =
        "Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=d:/db/SchoolManager.accdb";

    public void SetStudentCourse(int studentId, int courseId)
    {
        var isCourseAssignedToStudent = IsCourseAssignedToStudent(studentId, courseId);
        if (isCourseAssignedToStudent) return;
        
        using var connection = new OdbcConnection(ConnectionString);
        connection.Execute($"insert into StudentCourse (StudentId,CourseId) values({studentId},{courseId})");
    }

    public bool IsCourseAssignedToStudent(int studentId, int courseId)
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.QueryFirstOrDefault<int?>($"select Id from StudentCourse where StudentId = {studentId} and CourseId={courseId}");
        if (result != null && result > 0)
            return true;
        return false;
    }
    
    public List<StudentCourse> GetStudentCourses()
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Query<StudentCourse>("select Id, StudentId,CourseId from StudentCourse").ToList();

        return result;
    }

    public void UpdateStudentCourses(StudentCourse studentCourse)
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result =
            connection.Execute(
                $"insert into StudentCourse (StudentId,CourseId) values({studentCourse.StudentId},{studentCourse.CourseId}) where Id==({studentCourse.Id})");
    }

    public void DeleteStudentCourses(StudentCourse studentCourse)
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Execute($"delete from StudentCourse where Id==({studentCourse.Id})");
    }
    
}