using Dapper;
using Npgsql;
namespace SchoolManagement.DataLayer;

public class PostgresStudentCourseRepository:IStudentCourseRepository
{
    private string cs = "host=localhost;Username=postgres;Password=123456;Database=school_manager";
    public void SetStudentCourse(int studentId, int courseId)
    {
        using var con = new NpgsqlConnection(cs);
        con.Open();
        using var cmd = new NpgsqlCommand();
        cmd.Connection = con;
        cmd.CommandText = $"INSERT INTO student_course (student_id,course_id)  VALUES ({studentId},{courseId})";
        cmd.ExecuteNonQuery();
        con.Close();
        
    }

    public bool IsCourseAssignedToStudent(int studentId, int courseId)
    {
        throw new NotImplementedException();
    }

    public List<StudentCourse> GetStudentCourses()
    {
        using var con = new NpgsqlConnection(cs);
        con.Open();
        using var cmd = new NpgsqlCommand();
        var result = con.QueryFirstOrDefault("select id,student_id,course_id from student_course")
            .ToList();
        return result;
    }

    public void UpdateStudentCourses(StudentCourse studentCourse)
    {
        throw new NotImplementedException();
    }

    public void DeleteStudentCourses(StudentCourse studentCourse)
    {
        throw new NotImplementedException();
    }
}