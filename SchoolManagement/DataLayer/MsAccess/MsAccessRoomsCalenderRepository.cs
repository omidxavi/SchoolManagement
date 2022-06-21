using System.Data.Odbc;
using Dapper;

namespace SchoolManagement.DataLayer;

public class MsAccessRoomsCalenderRepository : IRoomsCalenderRepository
{
    private const string ConnectionString =
        "Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=d:/db/SchoolManager.accdb";

    public List<RoomsCalender> GetRoomsCalender()
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Query<RoomsCalender>("select Id,RoomId,CourseId,ClassDay,ClassTime from RoomsCalender").ToList();

        return result;
    }

    public void AddRoomsCalender(RoomsCalender roomsCalender)
    {
        //insert into Teacher (Name) values('Omid')
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Execute($"insert into RoomsCalender (RoomId,CourseId,[Day],[Time]) values({roomsCalender.RoomId},{roomsCalender.CourseId},{(int)roomsCalender.Day},{(int)roomsCalender.Time})");
    }

    public void UpdateRoomsCalender(RoomsCalender roomsCalender)
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Execute($"insert into RoomsCalender (RoomId,CourseId,Day,Time) values({roomsCalender.RoomId},{roomsCalender.CourseId}),{roomsCalender.Day},{roomsCalender.Time}) where Id==({roomsCalender.Id})");
    }

    public void DeleteRoomsCalender(RoomsCalender roomsCalender)
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Execute($"delete from RoomsCalender where Id==({roomsCalender.Id})");
    }
    
    public bool CheckRoomCalender(int roomId,int day,int time)
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.QueryFirstOrDefault<int?>($"select Id from RoomsCalender where RoomId = {roomId}  and  ClassDay={day} and ClassTime={time}");
        if (result != null && result > 0) 
            return true;
        return false;
    }

    public bool CheckStudentConflictInDays(int day,int time,int courseId)
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = GetRoomsCalender();
        if (result != null)
        {
            foreach (var item in result)
            {
                if (Convert.ToInt32(item.Day)==day  && Convert.ToInt32(item.Time)==time)
                {
                    using var con = new OdbcConnection(ConnectionString);
                    var c1 = con.QueryFirstOrDefault<List<int?>>($"select StudentId from StudentCourse where CourseId={courseId} ");
                    var c2 = con.QueryFirstOrDefault<List<int?>>($"select StudentId from StudentCourse where CourseId={item.CourseId} ");
                    var final = c1.Except(c2);
                    bool hasElement = final.Count() != c1.Count;
                    if (hasElement) return true;
                }
                
            }

        }
        return false;
    }
    public void SetRoomCalender(int roomId,int courseId,int day,int time)
    {
        var checkRoomCalender = CheckRoomCalender(roomId, day, time);
        var checkStudentConflictInDays = CheckStudentConflictInDays(day, time, courseId);
        if (checkRoomCalender || checkStudentConflictInDays )
        {
            Console.WriteLine("This Class Has Been Reserved Before,Please Try Again");
            Console.WriteLine("OR Your Student Can Not Be In 2 Class at Same Time");
        }
        using var connection = new OdbcConnection(ConnectionString);
        connection.Execute($"insert into RoomsCalender (RoomId,CourseId,ClassDay,ClassTime) values({roomId},{courseId},{day},{time})");
    } 
}