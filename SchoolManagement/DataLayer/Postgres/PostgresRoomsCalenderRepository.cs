using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper;
using Npgsql;

namespace SchoolManagement.DataLayer;

public class PostgresRoomsCalenderRepository : IRoomsCalenderRepository
{
    private string cs = "host=localhost;Username=postgres;Password=123456;Database=school_manager";

    public List<RoomsCalender> GetRoomsCalender()
    {
        try
        {
            using var con = new NpgsqlConnection(cs);
            con.Open();
            using var cmd = new NpgsqlCommand();
            cmd.Connection = con;
            var result = con.Query<RoomsCalender>("select id,room_id,course_id,class_day,class_time from rooms_calender")
                .ToList();
            return result;

        }
        catch
        {
            Console.WriteLine("Database is empty.......");
            return null;
        }
       
    }

    public void AddRoomsCalender(RoomsCalender roomsCalender)
    {
        throw new NotImplementedException();
    }

    public void UpdateRoomsCalender(RoomsCalender roomsCalender)
    {
        throw new NotImplementedException();
    }

    public void DeleteRoomsCalender(RoomsCalender roomsCalender)
    {
        throw new NotImplementedException();
    }

    public bool CheckRoomCalender(int roomId, int day, int time)
    {
        using var con = new NpgsqlConnection(cs);
        var result = con.QueryFirstOrDefault<int?>(
            $"select id from rooms_calender where room_id = {roomId}  and  class_day={day} and class_time={time}");
        if (result != null && result > 0)
            return true;
        return false;
    }

    public bool CheckStudentConflictInDays(int day, int time, int courseId)
    {
        using var con = new NpgsqlConnection(cs);
        var result = GetRoomsCalender();
        if (result != null)
        {
            foreach (var item in result)
            {
                if (Convert.ToInt32(item.Day) == day && Convert.ToInt32(item.Time) == time)
                {
                    var c1 = con.QueryFirstOrDefault<List<int?>>(
                        $"select student_id from student_course where course_id={courseId} ");
                    var c2 = con.QueryFirstOrDefault<List<int?>>(
                        $"select student_id from student_course where course_id={item.CourseId} ");
                    var final = c1.Except(c2);
                    bool hasElement = final.Count() != c1.Count;
                    if (hasElement) return true;
                }
            }
        }

        return false;
    }
    

    public void SetRoomCalender(int roomId, int courseId, int day, int time)
    {
        var checkRoomCalender = CheckRoomCalender(roomId, day, time);
        var checkStudentConflictInDays = CheckStudentConflictInDays(day, time, courseId);
        if (checkRoomCalender || checkStudentConflictInDays)
        {
            Console.WriteLine("This Class Has Been Reserved Before,Please Try Again");
            Console.WriteLine("OR Your Student Can Not Be In 2 Class at Same Time");
        }

        using var con = new NpgsqlConnection(cs);
        con.Open();
        using var cmd = new NpgsqlCommand();
        cmd.Connection = con;
        cmd.CommandText =
            ($"INSERT INTO rooms_calender (room_id,course_id,class_day,class_time) VALUES ({roomId},{courseId},{day},{time})");
        cmd.ExecuteNonQuery();
        con.Close();
    }
}