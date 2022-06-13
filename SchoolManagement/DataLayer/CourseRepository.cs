using System.Data.Odbc;
using Dapper;

namespace SchoolManagement.DataLayer;

public class CourseRepository
{
    private const string ConnectionString =
        "Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=d:/db/SchoolManager.accdb";

    public List<Course> GetCourses()
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Query<Course>("select  Id,Name from Course").ToList();

        return result;
    }

    public void AddCourses(Course course)
    {
        //insert into Teacher (Name) values('Omid')
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Execute($"insert into Course (Name) values('{course.Name}')");
    }

    public void UpdateCourses(Course course)
    {
        using var connection = new OdbcConnection(ConnectionString);
        connection.Execute($"insert into Course (Name,TeacherId) values ({course.Name},{course.TeacherId})");
    }

    public void DeleteCourses(Course course)
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Execute($"delete from Course where id in (select top 1 id from Course order by id desc )");
    }
}