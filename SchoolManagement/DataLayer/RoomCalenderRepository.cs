using System.Data.Odbc;
using Dapper;

namespace SchoolManagement.DataLayer;

public class RoomsCalenderRepository
{
    private const string ConnectionString =
        "Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=d:/db/SchoolManager.accdb";

    public List<RoomsCalender> GetRoomsCalender()
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Query<RoomsCalender>("select Id, RoomId,CourseId,Day,Time from RoomsCalender").ToList();

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
}