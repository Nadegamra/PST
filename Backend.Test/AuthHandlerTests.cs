using System.Security.Claims;
using System.Text.Json;
using Backend.Data.Views.User;
using Backend.Test.Mocks.Handlers;

namespace Backend.Test
{
    public class AuthHandlerTests
    {
        [Fact]
        public async void GetUserTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = AuthHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act

            // Assert
            Assert.Equal(1, 0);
        }
        [Fact]
        public async void LoginTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = AuthHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act

            // Assert
            Assert.Equal(1, 0);
        }
        [Fact]
        public async void LogoutTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = AuthHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act

            // Assert
            Assert.Equal(1, 0);
        }
        [Fact]
        public async void GetProfileTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = AuthHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act
            var claims = new List<Claim>()
            {
                new Claim("UserId", "3")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var actualResult = await handler.GetProfile(claimsPrincipal);
            var roleIds = dbMock.UserRoles.Where(x => x.UserId == 3).Select(x => x.RoleId).ToList();
            var roles = dbMock.Roles.Where(x => roleIds.Contains(x.Id)).ToList();
            var expectedResult = mapper.Map<UserGet>(dbMock.Users.First(x => x.Id == 3));
            expectedResult.Role = roles.First().Name;
            // Assert

            Assert.Equal(JsonSerializer.Serialize(expectedResult), JsonSerializer.Serialize(actualResult));

        }
        [Fact]
        public async void RegisterPhysicalLenderTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = AuthHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act

            // Assert
            Assert.Equal(1, 0);
        }
        [Fact]
        public async void RegisterLegalLenderTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = AuthHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act

            // Assert
            Assert.Equal(1, 0);
        }
        [Fact]
        public async void RegisterAsBorrowerTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = AuthHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act

            // Assert
            Assert.Equal(1, 0);
        }
        [Fact]
        public async void GetRegistrationRequestsTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = AuthHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act

            // Assert
            Assert.Equal(1, 0);
        }
        [Fact]
        public async void SubmitRegistrationRequestTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = AuthHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act

            // Assert
            Assert.Equal(1, 0);
        }
        [Fact]
        public async void ApproveRegistrationRequestTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = AuthHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act

            // Assert
            Assert.Equal(1, 0);
        }

    }
}