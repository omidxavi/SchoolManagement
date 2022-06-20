using Dapper;
using Npgsql;

namespace SchoolManagement.DataLayerPostgreSql;

public class CourseRepository
{
    private  const string _cs = "Host=localhost;Username=Postgres;password=123456;Database=postgreSql";

        public List<Course> GetCourses()
        {
            using var connection = new NpgsqlConnection(_cs);
            connection.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = connection; 
            var result = connection.Query<Course>("select  Id,Name from Course").ToList();
            connection.Close();
            return result;
        }

        public void AddCourses(Course course)
        {
            using var connection = new NpgsqlConnection(_cs);
            connection.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            string sql = "INSERT INTO Course (Name) VALUES ('{course.NAME }') ";
            connection.Close();

        }

        public void UpdateCourses(Course course)
        {
            using var connection = new NpgsqlConnection(_cs);
            connection.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = connection;
            string sql = "insert into Course (Name,TeacherId) values ('{course.Name}',{course.TeacherId})";
            connection.Close();

        }
}