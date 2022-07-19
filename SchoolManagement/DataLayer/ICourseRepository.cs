namespace SchoolManagement.DataLayer;

public interface ICourseRepository
{
    List<Course> GetCourses();

    void AddCourses(Course course);

    void UpdateCourses(Course course);

    void DeleteCourses(Course course);
}