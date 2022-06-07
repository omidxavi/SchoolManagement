using System.Runtime.InteropServices;

namespace SchoolManagement;

public class RoomManager
{
    private readonly List<Room> _rooms;
    private readonly IdGeneratorRoom _idGeneratorRoom;

    public RoomManager()
    {
        _rooms = new CsvManager().GetRoom();
        _idGeneratorRoom = new IdGeneratorRoom(_rooms.Count);
    }

    public Room GetRoomFromUser()
    {
        var id = _idGeneratorRoom.GenerateId();
        Console.WriteLine("please Enter your room number");
        var number = int.Parse(Console.ReadLine());
        var room = new Room(id: id, number: number);
        Print(room);
        AddToList(room);
        AddToExcel(room);
        return room;
    }
    
    
    private void Print(Room room)
    {
        Console.WriteLine($"{room.Id} : {room.Number } ");
    }

    private void AddToExcel(Room room)
    {
        CsvManager csvManager = new CsvManager();
        csvManager.AddToRoomsFile(room);
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
            Console.WriteLine($"{i+1} -> {room.Id} , {room.Number}");

        }

        var input = Console.ReadLine();
        var selectIndex = int.Parse(input);
        var selectRoom = _rooms[selectIndex - 1];
        return selectRoom;
    }
}