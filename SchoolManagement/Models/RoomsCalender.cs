namespace SchoolManagement;

public class RoomsCalender
{
    public int Id;
    public int RoomId;
    public int CourseId;
    public Days Day;
    public Times Time;

    public enum Days
    {
        satarday,
        sunday ,
        monday,
        tuesday,
        wednesday,
        thursday,
        friday 
    }
    public enum Times
    {
       first,
       second,
       third,
       forth
    }
    
   /* public RoomsCalender(int roomId,int courseId,int day,int time)
    {
        RoomId = roomId;
        CourseId =courseId;
        Day = (Days) day;
        Time = (Times) time;
    }*/
    
    
    
    public void AssignCourse(Course course)
    {
        CourseId=course.Id;
        Console.WriteLine($" course {course.Name} assigned to {CourseId} in RoomCalender");
    }
    
    public void AssignRoom(Room room)
    {
        RoomId=room.Id;
        Console.WriteLine($"Room {room.Number} assigned to {RoomId}Room");
    }
    

}
