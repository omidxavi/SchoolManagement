using System.Data.Odbc;
using Dapper;

namespace SchoolManagement.DataLayer;

public interface IStudentRepository
{
    public List<Student> GetStudents();

    public void AddStudents(Student student);


    public void UpdateStudents(Student student);


    public void DeleteStudents(Student student);

}