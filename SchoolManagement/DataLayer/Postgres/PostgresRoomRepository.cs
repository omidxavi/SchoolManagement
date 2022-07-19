using Npgsql;
using Dapper;

namespace SchoolManagement.DataLayer;

public class PostgresRoomRepository : IRoomRepository
{
    private string cs = "host=localhost;Username=postgres;Password=123456;Database=school_manager";
    public List<Room> GetRooms()
    {
        try
        {
            using var con = new NpgsqlConnection(cs);
            con.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;
            var result = con.Query<Room>("select id,rooms_number from room")
                .ToList();
            return result;

        }
        catch
        {
            Console.WriteLine("Database is empty.......");
            return null;
        }
    }

    public void AddRooms(Room room)
    {
        using var con = new NpgsqlConnection(cs);
        con.Open();
        using var cmd = new NpgsqlCommand();
        cmd.Connection = con;
        cmd.CommandText = $"INSERT INTO room (rooms_number)  VALUES ({room.RoomsNumber})";
        cmd.ExecuteNonQuery();
        con.Close();
        
    }

    public void UpdateRooms(Room room)
    {
        throw new NotImplementedException();
    }

    public void DeleteRooms(Room room)
    {
        throw new NotImplementedException();
    }
}





