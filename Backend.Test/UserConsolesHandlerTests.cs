
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using AutoMapper;
using Backend.Data.Views.Image;
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
            var mapper = AutomapperMock.GetMock();
            var handler = UserConsolesHandlerMock.GetMock(dbMock);
            var claims = new List<Claim>()
            {
                new Claim("UserId", "2")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            // Act
            var actualResult = await handler.GetUserConsolesByStatusAsync(claimsPrincipal, UserConsoleStatus.UNCONFIRMED);
            var expectedResult = mapper.Map<List<UserConsole>, List<UserConsoleGetDto>>(dbMock.UserConsoles.Where(x => x.UserId == 2 && x.ConsoleStatus == UserConsoleStatus.UNCONFIRMED).ToList());

            // Assert
            Assert.Equal(JsonSerializer.Serialize(actualResult), JsonSerializer.Serialize(expectedResult));
        }
        [Fact]
        public async void GetUserConsoleAsyncTest()
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
            var actualResult = await handler.GetUserConsoleAsync(2);
            var expectedResult = mapper.Map<UserConsole, UserConsoleGetDto>(dbMock.UserConsoles.Where(x => x.Id == 2).First());
            // Assert
            Assert.Equal(JsonSerializer.Serialize(actualResult), JsonSerializer.Serialize(expectedResult));
        }
        [Fact]
        public async void AddUserConsoleAsyncTest()
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
            // ActValueTask.FromResult(
            var images = new List<ImageAddDto>() { new ImageAddDto { Name = "name", Description = "description", Stream = Convert.ToBase64String(Encoding.UTF8.GetBytes("stream")) } };
            await handler.AddUserConsoleAsync(new UserConsoleAddDto { ConsoleId = 2, Amount = 1, Accessories = "none", Images = images }, claimsPrincipal);
            var actualResult = dbMock.UserConsoles.Last();
            var addedImages = new List<Image> { dbMock.Images.Last() };
            // Assert
            Assert.Equal(2, actualResult.ConsoleId);
            Assert.Equal(1, actualResult.Amount);
            Assert.Equal("none", actualResult.Accessories);
            Assert.Equal(JsonSerializer.Serialize(actualResult.Images), JsonSerializer.Serialize(addedImages));
        }
        [Fact]
        public async void UpdateUserConsoleAsyncTest()
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
            await handler.UpdateUserConsoleAsync(new UserConsoleUpdateDto { Id = 4, Accessories = "updated", Amount = 4, ConsoleId = 3, Images = new List<ImageUpdateDto>() });
            var actualResult = dbMock.UserConsoles.Where(x => x.Id == 4).First();
            // Assert
            Assert.Equal("updated", actualResult.Accessories);
            Assert.Equal(4, actualResult.Amount);
        }
        [Fact]
        public async void RemoveUserConsoleAsyncTest()
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
            await handler.RemoveUserConsoleAsync(6);
            var actualResult = dbMock.UserConsoles.Where(x => x.Id == 6).FirstOrDefault();
            // Assert
            Assert.Null(actualResult);
        }
        [Fact]
        public async void UpdateStatusTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var mapper = AutomapperMock.GetMock();
            var handler = UserConsolesHandlerMock.GetMock(dbMock);
            var claims = new List<Claim>()
            {
                new Claim("UserId", "1")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            // Act
            await handler.UpdateStatus(new UserConsoleStatusUpdateDto { Id = 1, ConsoleStatus = UserConsoleStatus.AT_PLATFORM }, claimsPrincipal);
            var userConsole = dbMock.UserConsoles.Where(x => x.Id == 1).First();
            // Assert
            Assert.Equal(UserConsoleStatus.AT_PLATFORM, userConsole.ConsoleStatus);
        }
    }
}