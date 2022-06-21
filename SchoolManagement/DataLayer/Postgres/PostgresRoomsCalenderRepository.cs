namespace SchoolManagement.DataLayer;

public class PostgresRoomsCalenderRepository : IRoomsCalenderRepository
{
    public List<RoomsCalender> GetRoomsCalender()
    {
        throw new NotImplementedException();
    }

    public void AddRoomsCalender(RoomsCalender roomsCalender)
    {
        throw new NotImplementedException();
    }

    public void UpdateRoomsCalender(RoomsCalender roomsCalender)
    {
        throw new NotImplementedException();
    }

    public void DeleteRoomsCalender(RoomsCalender roomsCalender)
    {
        throw new NotImplementedException();
    }

    public bool CheckRoomCalender(int roomId, int day, int time)
    {
        throw new NotImplementedException();
    }

    public bool CheckStudentConflictInDays(int day, int time, int courseId)
    {
        throw new NotImplementedException();
    }

    public void SetRoomCalender(int roomId, int courseId, int day, int time)
    {
        throw new NotImplementedException();
    }
}