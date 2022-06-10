using System.Data.Odbc;
using Dapper;

namespace SchoolManagement.DataLayer;

public class StudentRepository
{
    private const string ConnectionString =
        "Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=d:/db/SchoolManager.accdb";

    public List<Student> GetStudents()
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Query<Student>("select Id, Name,Family from Student").ToList();

        return result;
    }

    public void AddStudents(Student student)
    {
        //insert into Teacher (Name) values('Omid')
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Execute($"insert into Student (Name,Family) values('{student.Name}','{student.Family}')");
    }

    public void UpdateStudents(Student student)
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Execute($"insert into Student (Name,Family) values('{student.Name}','{student.Family}') where Id==({student.Id})");
    }

    public void DeleteStudents(Student student)
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Execute($"delete from Student where Id==({student.Id})");
    }
}