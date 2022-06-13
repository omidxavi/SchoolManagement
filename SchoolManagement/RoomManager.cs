using System.Runtime.InteropServices;
using SchoolManagement.DataLayer;

namespace SchoolManagement;

public class RoomManager
{
    private readonly List<Room> _rooms;

    public RoomManager()
    {
        _rooms = new CsvManager().GetRoom();
    }

    public Room GetRoomFromUser()
    {
        Console.WriteLine("please Enter your room number");
        var number = int.Parse(Console.ReadLine());
        var room = new Room()
        {
            RoomsNumber = number
        };
        Print(room);
        AddToList(room);
        var roomRepository = new RoomRepository();
        try
        {
            var isExist = roomRepository.GetRooms().Exists(x => x.RoomsNumber == number);

            if (!isExist)
            {
                roomRepository.AddRooms(room);
                Console.WriteLine("your course added...");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        return room;
    }
    
    
    private void Print(Room room)
    {
        Console.WriteLine($"{room.Id} : {room.RoomsNumber } ");
    }
    
    public void AddToList(Room room)
    {
        _rooms.Add(room);
    }
    
    public void PrintRooms()
    {
        Console.WriteLine("-------Rooms--------------------------");
        foreach (var room in _rooms)
        {
            Print(room);
            Console.WriteLine("**************************");
        }    
        Console.WriteLine("-----------------------------------------");
    }
    
    public Room SelectRoom()
    {
        Console.WriteLine("select your Room");
        for (int i = 0; i < _rooms.Count; i++)
        {
            var room = _rooms[i];
            Console.WriteLine($"{i+1} -> {room.Id} , {room.RoomsNumber}");

        }

        var input = Console.ReadLine();
        var selectIndex = int.Parse(input);
        var selectRoom = _rooms[selectIndex - 1];
        return selectRoom;
    }
}