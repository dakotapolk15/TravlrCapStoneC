
using Xunit;
using travlr.Backend.Models;

public class UserTests
{
    [Fact]
    public void User_ShouldCreateInstance()
    {
        // Act
        var user = new User();

        // Assert
        Assert.NotNull(user);
    }
}
