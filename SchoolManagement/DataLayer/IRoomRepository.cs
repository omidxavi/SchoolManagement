namespace SchoolManagement.DataLayer;

public interface IRoomRepository
{
    public List<Room> GetRooms();

    public void AddRooms(Room room);

    public void UpdateRooms(Room room);

    public void DeleteRooms(Room room);
}