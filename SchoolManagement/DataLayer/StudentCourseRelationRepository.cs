using System.Data.Odbc;
using Dapper;

namespace SchoolManagement.DataLayer;

public class StudentCourseRepository
{
    private const string ConnectionString =
        "Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=d:/db/SchoolManager.accdb";

    public List<StudentCourse> GetStudentCourses()
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Query<StudentCourse>("select Id, StudentId,CourseId from StudentCourse").ToList();

        return result;
    }

    public void AddStudentCourses(StudentCourse studentCourse)
    {
        //insert into Teacher (Name) values('Omid')
        using var connection = new OdbcConnection(ConnectionString); 
        connection.Execute($"insert into StudentCourse (StudentId,CourseId) values('{studentCourse.StudentId}','{studentCourse.CourseId}')");
    }

    public void UpdateStudentCourses(StudentCourse studentCourse)
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Execute($"insert into StudentCourse (StudentId,CourseId) values('{studentCourse.StudentId}','{studentCourse.CourseId}') where Id==({studentCourse.Id})");
    }

    public void DeleteStudentCourses(StudentCourse studentCourse)
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Execute($"delete from StudentCourse where Id==({studentCourse.Id})");
    }
}