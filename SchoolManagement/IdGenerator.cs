namespace SchoolManagement;

public class IdGeneratorTeacher
{
    private int i;

    public IdGeneratorTeacher(int start)
    {
        i = start;
    }

    public int GenerateId()
    {
        i = i + 1;
        return i;
    }
}

public class IdGeneratorCourse
{
    private int i;

    public IdGeneratorCourse(int start)
    {
        i = start;
    }

    public int GenerateId()
    {
        i = i + 1;
        return i;

    }

}

public class IdGeneratorStudent
{
    private int i;

    public IdGeneratorStudent(int start)
    {
        i = start;
    }

    public int  GenerateId()
    {
        i = i + 1;
        return i;
    }
}
public class IdGeneratorStudentCourse
{
    private int i;

    public IdGeneratorStudentCourse(int start)
    {
        i = start;
    }

    public int  GenerateId()
    {
        i = i + 1;
        return i;
    }
}