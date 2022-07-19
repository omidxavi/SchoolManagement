using System.Diagnostics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using SchoolManagement.DataLayer;

namespace SchoolManagement;

public class RoomManager
{
    private readonly List<Room> _rooms;
    private readonly IRoomRepository _roomRepository;

    public RoomManager(IRoomRepository roomRepository)
    {
        //var roomRepository = new MsAccessRoomRepository();
        _rooms = _roomRepository?.GetRooms();
        _roomRepository = roomRepository;
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
        //var roomRepository = new MsAccessRoomRepository();
        try
        {
            var isExist = _roomRepository.GetRooms().Exists(x => x.RoomsNumber == number);

            if (!isExist)
            {
                _roomRepository.AddRooms(room);
                Console.WriteLine("your course added...");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e); 
        }
        return room;
    }
    
    
    private void Print(Room room)
    {
        Console.WriteLine($"{room.Id} : {room.RoomsNumber } ");
    }
    
    public void AddToList(Room room)
    {
        if (_rooms != null) _rooms.Add(room);
    }
    
    public void PrintRooms()
    {
        Console.WriteLine("-------Rooms--------------------------");
        if (_rooms != null)
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
        for (int i = 0; i < _roomRepository.GetRooms().Count; i++)
        {
            var room = _roomRepository.GetRooms()[i];
            Console.WriteLine($"{i+1} -> {room.Id} , {room.RoomsNumber}");

        }

        var input = Console.ReadLine();
        var selectIndex = int.Parse(input);
        var selectRoom = _roomRepository.GetRooms()[selectIndex - 1];
        return selectRoom;
    }
}