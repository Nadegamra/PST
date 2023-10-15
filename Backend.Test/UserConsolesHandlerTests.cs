
using System.Security.Claims;
using System.Text.Json;
using AutoMapper;
using Backend.Data.Views.UserConsole;
using Backend.Test.Mocks.Handlers;

namespace Backend.Test
{
    public class UserConsolesHandlerTests
    {
        [Fact]
        public async void GetUserConsolesAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var mapper = AutomapperMock.GetMock();
            var handler = UserConsolesHandlerMock.GetMock(dbMock);
            var claims = new List<Claim>()
            {
                new Claim("UserId", "2")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            // Act
            var actualResult = await handler.GetUserConsolesAsync(claimsPrincipal);
            var expectedResult = mapper.Map<List<UserConsole>, List<UserConsoleGetDto>>(dbMock.UserConsoles.Where(x => x.UserId == 2).ToList());
            // Assert
            Assert.Equal(JsonSerializer.Serialize(actualResult), JsonSerializer.Serialize(expectedResult));

        }
        [Fact]
        public async void GetUserConsolesByStatusAsync()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = UserConsolesHandlerMock.GetMock(dbMock);
            var claims = new List<Claim>()
            {
                new Claim("UserId", "2")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            // Act

            // Assert
            Assert.Equal(1, 0);
        }
        [Fact]
        public async void GetUserConsoleAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = UserConsolesHandlerMock.GetMock(dbMock);
            var claims = new List<Claim>()
            {
                new Claim("UserId", "2")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            // Act

            // Assert
            Assert.Equal(1, 0);
        }
        [Fact]
        public async void AddUserConsoleAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = UserConsolesHandlerMock.GetMock(dbMock);
            var claims = new List<Claim>()
            {
                new Claim("UserId", "2")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            // Act

            // Assert
            Assert.Equal(1, 0);
        }
        [Fact]
        public async void UpdateUserConsoleAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = UserConsolesHandlerMock.GetMock(dbMock);
            var claims = new List<Claim>()
            {
                new Claim("UserId", "2")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            // Act

            // Assert
            Assert.Equal(1, 0);
        }
        [Fact]
        public async void RemoveUserConsoleAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = UserConsolesHandlerMock.GetMock(dbMock);
            var claims = new List<Claim>()
            {
                new Claim("UserId", "2")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            // Act

            // Assert
            Assert.Equal(1, 0);
        }
        [Fact]
        public async void UpdateStatusTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = UserConsolesHandlerMock.GetMock(dbMock);
            var claims = new List<Claim>()
            {
                new Claim("UserId", "2")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            // Act

            // Assert
            Assert.Equal(1, 0);
        }
    }
}