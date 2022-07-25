using Dapper;
using Npgsql;
namespace SchoolManagement.DataLayer;

public class PostgresCourseRepository : ICourseRepository
{
    private string cs = "host=localhost;Username=postgres;Password=123456;Database=school_manager";

    public List<Course> GetCourses()
    {
        try
        {
            using var con = new NpgsqlConnection(cs);
            con.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;
            var result =con.Query<Course> ("select id,name,teacher_id from course")
                .ToList();
            return result;

        }
        catch
        {
            Console.WriteLine("Database is empty.......");
            return null;
        }
    }

    public void AddCourses(Course course)
    {
        using var con = new NpgsqlConnection(cs);
        con.Open();
        using var cmd = new NpgsqlCommand();
        cmd.Connection = con;
        cmd.CommandText = $"INSERT INTO course (name)  VALUES ('{course.Name}')";
        cmd.ExecuteNonQuery();
        con.Close();
    }

    public void UpdateCourses(Course course)
    {
        using var con = new NpgsqlConnection(cs);
        con.Open();
        using var cmd = new NpgsqlCommand();
        cmd.Connection = con;
        cmd.CommandText=$"UPDATE  course SET name='{course.Name}',teacher_id={course.TeacherId}  where name='{course.Name}'";
        cmd.ExecuteNonQuery();
        con.Close();
    }
    public void DeleteCourses(Course course)
    {
        throw new NotImplementedException();
    }
}