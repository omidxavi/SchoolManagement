namespace SchoolManagement.DataLayer;

public interface IRoomsCalenderRepository
{
    List<RoomsCalender> GetRoomsCalender();
    void AddRoomsCalender(RoomsCalender roomsCalender);
    void UpdateRoomsCalender(RoomsCalender roomsCalender);
    void DeleteRoomsCalender(RoomsCalender roomsCalender);
    bool CheckRoomCalender(int roomId,int day,int time);
    bool CheckStudentConflictInDays(int day,int time,int courseId);
    void SetRoomCalender(int roomId,int courseId,int day,int time);
}