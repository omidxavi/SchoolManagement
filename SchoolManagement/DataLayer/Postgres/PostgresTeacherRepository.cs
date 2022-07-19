using Npgsql;
using Dapper;
namespace SchoolManagement.DataLayer;

public class PostgresTeacherRepository : ITeacherRepository
{
    private string cs = "host=localhost;Username=postgres;Password=123456;Database=school_manager";

    public List<Teacher> GetTeachers()
    {
        try
        {
            using var con = new NpgsqlConnection(cs);
            con.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;
            var result = con.Query<Teacher>("select id,name,family from teacher")
                .ToList();
            return result;

        }
        catch
        {
            Console.WriteLine("Database is empty.......");
            return null;
        }
    }

    public void AddTeacher(Teacher teacher)
    {
        using var con = new NpgsqlConnection(cs);
        con.Open();
        using var cmd = new NpgsqlCommand();
        cmd.Connection = con;
        cmd.CommandText = $"INSERT INTO teacher (name,family)  VALUES ('{teacher.Name}','{teacher.Family}')";
        cmd.ExecuteNonQuery();
        con.Close();
    }

    public void UpdateTeacher(Teacher teacher)
    {
        throw new NotImplementedException();
    }

    public void DeleteTeacher(Teacher teacher)
    {
        throw new NotImplementedException();
    }
}