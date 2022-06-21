using SchoolManagement.DataLayer;
using Xunit;
namespace SchoolManagement.Test;

public class RoomCalenderRepositoryTest
{
    [Fact]
    public void IsRoomCalenderShouldAddToDb()
    {
        var repository = new MsAccessRoomsCalenderRepository();
        var result = repository.CheckRoomCalender(5,5,5);
        Assert.False(result);
    }
}