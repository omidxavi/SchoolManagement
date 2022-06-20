using System.Data.Odbc;
using Dapper;

namespace SchoolManagement.DataLayer;

public class TeacherRepository
{
    private const string ConnectionString =
        "Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=d:/db/SchoolManager.accdb";

    public List<Teacher> GetTeachers()
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Query<Teacher>("select Id, Name,Family from Teacher").ToList();
        return result;
    }

    public void AddTeacher(Teacher teacher)
    {
        //insert into Teacher (Name) values('Omid')
        using var connection = new OdbcConnection(ConnectionString);
        var result=connection.Execute($"insert into Teacher (Name,Family) values('{teacher.Name}','{teacher.Family}')");
    }

    public void UpdateTeacher(Teacher teacher)
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Execute($"insert into Teacher (Name,Family) values({teacher.Name},{teacher.Family}) where Id==({teacher.Id})");
    }

    public void DeleteTeacher(Teacher teacher)
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Execute($"delete from Teacher where Id==({teacher.Id})");
    }
}