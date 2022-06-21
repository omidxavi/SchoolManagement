using SchoolManagement.DataLayer;
using System.Data.Odbc;
using Dapper;

namespace SchoolManagement;


public class RoomCalenderManager

{
    private readonly List<RoomsCalender> _roomsCalender;
    public RoomCalenderManager()
    {
        _roomsCalender = new CsvManager().GetRoomsCalender(); 
    }
    private const string ConnectionString =
        "Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=d:/db/SchoolManager.accdb";


    public RoomsCalender DefineGeneralCalender(RoomManager roomManager, CourseManager courseManager)
    {
        var roomId = roomManager.SelectRoom().Id;
        var courseId = courseManager.SelectCourse().Id;
        Console.WriteLine("chose your day '0=>sat , 1=>sun , ...6=>fri' ");
        var day = int.Parse(Console.ReadLine());
        Console.WriteLine("chose your time '0=>8:10 , 1=>10:12 , 2=>13:15 , 3=>15:17' ");
        var time = int.Parse(Console.ReadLine());
        var roomsCalender = new RoomsCalender()
        {
            RoomId = roomId,
            CourseId = courseId,
            Day = (RoomsCalender.Days) day,
            Time = (RoomsCalender.Times) time
        };
        Print(roomsCalender);
        AddToList(roomsCalender);
        IRoomsCalenderRepository roomsCalenderRepository = new PostgresRoomsCalenderRepository();
        roomsCalenderRepository.SetRoomCalender(roomId, courseId, day, time);
        return roomsCalender;

    }
    
    
    private void Print(RoomsCalender roomsCalender)
    {
        Console.WriteLine($" this id : {roomsCalender.Id} assigned to : {roomsCalender.Day} ");
    }
    
    public void AddToList(RoomsCalender roomsCalender)
    {
        _roomsCalender.Add(roomsCalender);
    }
    
    
    

}