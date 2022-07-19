using System.Data.Odbc;
using Dapper;
namespace SchoolManagement.DataLayer;

public class MsAccessRoomRepository
{

    
    private const string ConnectionString =
        "Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=d:/db/SchoolManager.accdb";

    public List<Room> GetRooms()
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Query<Room>("select Id,RoomsNumber from Room").ToList();

        return result;
    }

    public void AddRooms(Room room)
    {
        //insert into Teacher (Name) values('Omid')
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Execute($"insert into Room (RoomsNumber) values({room.RoomsNumber})");
    }

    public void UpdateRooms(Room room)
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Execute($"insert into Room (RoomsNumber) values ({room.RoomsNumber})  where Id==({room.Id})");
    }

    public void DeleteRooms(Room room)
    {
        using var connection = new OdbcConnection(ConnectionString);
        var result = connection.Execute($"delete from Room where Id==({room.Id})");
    }
}
