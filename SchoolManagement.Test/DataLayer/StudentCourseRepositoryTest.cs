using SchoolManagement.DataLayer;
using Xunit;

namespace SchoolManagement.Test;

public class StudentCourseRepositoryTest
{
    [Fact]
    public void SetStudentCourse_Should_AssignCourseToStudent()
    {
        var repository = new StudentCourseRepository();
        repository.SetStudentCourse(1, 2);
    }
    
    [Fact]
    public void IsCourseAssignedToStudent_Should_ShouldCheckRelation()
    {
        var repository = new StudentCourseRepository();
        
        var result = repository.IsCourseAssignedToStudent(-1, -1);
        Assert.False(result);
    }
}