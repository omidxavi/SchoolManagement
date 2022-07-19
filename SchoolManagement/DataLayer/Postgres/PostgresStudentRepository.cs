using Npgsql;
using Dapper;
namespace SchoolManagement.DataLayer;

public class PostgresStudentRepository : IStudentRepository
{
    private string cs = "host=localhost;Username=postgres;Password=123456;Database=school_manager";

    public List<Student> GetStudents()
    {
        try
        {
            using var con = new NpgsqlConnection(cs);
            con.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;
            var result = con.Query<Student>("select id,name,family from student")
                .ToList();
            return result;
        }
        catch
        {
            Console.WriteLine("Database is empty.......");
            return null;
        }
    }

    public void AddStudents(Student student)
    {
        using var con = new NpgsqlConnection(cs);
        con.Open();
        using var cmd = new NpgsqlCommand();
        cmd.Connection = con;
        cmd.CommandText = $"INSERT INTO student (name,family)  VALUES ('{student.Name}','{student.Family}')";
        cmd.ExecuteNonQuery();
        con.Close();
    }

    public void UpdateStudents(Student student)
    {
        throw new NotImplementedException();
    }

    public void DeleteStudents(Student student)
    {
        throw new NotImplementedException();
    }
}