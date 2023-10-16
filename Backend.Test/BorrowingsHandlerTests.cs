using System.Security.Claims;
using System.Text.Json;
using Backend.Data.Views.BorrowedConsole;
using Backend.Test.Mocks.Handlers;

namespace Backend.Test
{
    public class BorrowingsHandlerTests
    {
        [Fact]
        public async void GetAllAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var mapper = AutomapperMock.GetMock();
            var claims = new List<Claim>()
            {
                new Claim("UserId", "2")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var handler = BorrowingsHandlerMock.GetMock(dbMock);
            // Act
            var actualResult = await handler.GetAllAsync();
            var expectedResult = mapper.Map<List<Borrowing>, List<BorrowingGetDto>>(dbMock.Borrowings.ToList());
            // Assert
            Assert.Equal(JsonSerializer.Serialize(expectedResult), JsonSerializer.Serialize(actualResult));
        }
        [Fact]
        public async void GetByUserAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var mapper = AutomapperMock.GetMock();
            var claims = new List<Claim>()
            {
                new Claim("UserId", "3")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var handler = BorrowingsHandlerMock.GetMock(dbMock);
            // Act
            var actualResult = await handler.GetByUserAsync(claimsPrincipal);
            var expectedResult = mapper.Map<List<Borrowing>, List<BorrowingGetDto>>(dbMock.Borrowings.Where(x => x.UserId == 3).ToList());
            // Assert
            Assert.Equal(JsonSerializer.Serialize(expectedResult), JsonSerializer.Serialize(actualResult));
        }
        [Fact]
        public async void AddAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var mapper = AutomapperMock.GetMock();
            var claims = new List<Claim>()
            {
                new Claim("UserId", "2")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var handler = BorrowingsHandlerMock.GetMock(dbMock);
            // Act
            await handler.AddAsync(new BorrowingAddDto { UserConsoleIds = new List<int> { 8, 12, 14 } }, claimsPrincipal);
            var borrowing = dbMock.Borrowings.Last();
            // Assert
            Assert.Equal(5, borrowing.Id);
            Assert.Equal(5, dbMock.UserConsoles.First(x => x.Id == 8).BorrowingId);
            Assert.Equal(5, dbMock.UserConsoles.First(x => x.Id == 12).BorrowingId);
            Assert.Equal(5, dbMock.UserConsoles.First(x => x.Id == 14).BorrowingId);
        }
        [Fact]
        public async void UpdateAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var mapper = AutomapperMock.GetMock();
            var claims = new List<Claim>()
            {
                new Claim("UserId", "1")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var handler = BorrowingsHandlerMock.GetMock(dbMock);
            // Act
            await handler.UpdateAsync(new BorrowingUpdateDto { Id = 3, UserConsoleIds = new List<int>() { } }, claimsPrincipal);
            // Assert
            Assert.Null(dbMock.UserConsoles.First(x => x.Id == 15).BorrowingId);
            Assert.Null(dbMock.UserConsoles.First(x => x.Id == 16).BorrowingId);
            Assert.Null(dbMock.UserConsoles.First(x => x.Id == 17).BorrowingId);
        }
        [Fact]
        public async void UpdateStatusAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var mapper = AutomapperMock.GetMock();
            var claims = new List<Claim>()
            {
                new Claim("UserId", "1")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var handler = BorrowingsHandlerMock.GetMock(dbMock);
            // Act
            await handler.UpdateStatusAsync(new BorrowingUpdateStatusDto { Id = 2, BorrowingStatus = BorrowingStatus.ACTIVE });
            // Assert
            Assert.Equal(BorrowingStatus.ACTIVE, dbMock.Borrowings.First(x => x.Id == 2).Status);

        }
        [Fact]
        public async void DeleteAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var mapper = AutomapperMock.GetMock();
            var claims = new List<Claim>()
            {
                new Claim("UserId", "1")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var handler = BorrowingsHandlerMock.GetMock(dbMock);
            // Act
            await handler.UpdateAsync(new BorrowingUpdateDto { Id = 3, UserConsoleIds = new List<int>() { } }, claimsPrincipal);
            await handler.DeleteAsync(3);
            // Assert
            Assert.Null(dbMock.Borrowings.FirstOrDefault(x => x.Id == 3));
        }
        [Fact]
        public async void CanDeleteTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var mapper = AutomapperMock.GetMock();
            var claims = new List<Claim>()
            {
                new Claim("UserId", "2")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var handler = BorrowingsHandlerMock.GetMock(dbMock);
            // Assert
            Assert.False(handler.CanDelete(1));
            Assert.False(handler.CanDelete(2));
            Assert.False(handler.CanDelete(3));
            Assert.True(handler.CanDelete(4));
        }
    }
}