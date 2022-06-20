using SchoolManagement.DataLayer;
using Xunit;
namespace SchoolManagement.Test;

public class RoomCalenderManagerTest
{
    [Fact]
    public void IsRoomCalenderShouldAddToDb()
    {
        var repository = new RoomCalenderManager();
        var result = repository.CheckRoomCalender(5,5,5);
        Assert.False(result);
    }
}