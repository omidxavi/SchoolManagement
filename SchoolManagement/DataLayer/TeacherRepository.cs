using System.Data.Odbc;
using Dapper;

namespace SchoolManagement.DataLayer;

public class TeacherRepository
{
    private const string ConnectionString = "Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=d:/db/SchoolManager.accdb";
    public List<Teacher> GetTeachers()
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Query<Teacher>("select Id, Name from Teacher").ToList();

        return result;
    }

    public void AddTeacher(Teacher teacher)
    {
        //insert into Teacher (Name) values('Omid')
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Execute($"insert into Teacher (Name) values({teacher.Name})");
    }
}