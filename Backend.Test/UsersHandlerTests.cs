using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Backend.Test
{
    public class UsersHandlerTests
    {
        [Fact]
        public async void GetUsersTest()
        {
            // Arrange
            var dbMock = DbContextMock.MockDbContext();
            var userManagerMock = UserManagerMock.MockUserManager(MockData.Users.ToList());
            var configMock = new Mock<IOptions<SmtpConfig>>();
            var handler = new UsersHandler(userManagerMock.Object, dbMock.Object, configMock.Object);
            // Act
            var result = await handler.GetUsers(null);
            var result2 = await handler.GetUsers("admin");
            // Assert
            Assert.True(JsonSerializer.Serialize(result) == JsonSerializer.Serialize(MockData.Users));
            Assert.True(JsonSerializer.Serialize(result2) == JsonSerializer.Serialize(new List<User> { MockData.Users[0] }));
        }
    }
}