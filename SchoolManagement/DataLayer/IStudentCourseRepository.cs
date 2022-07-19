using System.Data.Odbc;
using Dapper;

namespace SchoolManagement.DataLayer;

public interface IStudentCourseRepository
{

    public void SetStudentCourse(int studentId, int courseId);


    public bool IsCourseAssignedToStudent(int studentId, int courseId);


    public List<StudentCourse> GetStudentCourses();


    public void UpdateStudentCourses(StudentCourse studentCourse);


    public void DeleteStudentCourses(StudentCourse studentCourse);

}