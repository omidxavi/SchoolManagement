using System.Data.Odbc;
using Dapper;

namespace SchoolManagement.DataLayer;

public interface ITeacherRepository
{
    public List<Teacher> GetTeachers();


    public void AddTeacher(Teacher teacher);

    public void UpdateTeacher(Teacher teacher);


    public void DeleteTeacher(Teacher teacher);
}