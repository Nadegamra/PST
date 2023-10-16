using System.Text;
using System.Text.Json;
using Backend.Data.Views.Console;
using Backend.Data.Views.BorrowedConsole;
using Backend.Data.Views.Image;
using Backend.Test.Mocks.Handlers;
using System.Security.Claims;
using Backend.Data.Views.UserConsole;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Interfaces;

namespace Backend.Test
{
    public class BorrowingsHandlerTests
    {
        [Fact]
        public async void GetAllAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = BorrowingsHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act
            var actualResult = await handler.GetAllAsync();
            var expectedResult = mapper.Map<List<Data.Models.Borrowing>, List<BorrowingGetDto>>(dbMock.Borrowings);
            // Assert
            Assert.Equal(JsonSerializer.Serialize(expectedResult), JsonSerializer.Serialize(actualResult));
        }


        [Fact]
        public async void GetByUserAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = BorrowingsHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            var claims = new List<Claim>()
            {
                new Claim("UserId", "2")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            // Act
            var actualResult = await handler.GetByUserAsync(claimsPrincipal);
            var expectedResult = mapper.Map<List<Borrowing>, List<BorrowingGetDto>>(dbMock.Borrowings.Where(x => x.UserId == 2).ToList());
            // Assert
            Assert.Equal(JsonSerializer.Serialize(expectedResult), JsonSerializer.Serialize(actualResult));
        }


        [Fact]
        public async void GetByIdAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = BorrowingsHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act
            var actualResult = await handler.GetByIdAsync(2);
            var expectedResult = mapper.Map<Borrowing, BorrowingGetDto>(dbMock.Borrowings.Where(x => x.Id == 2).First());
            // Assert
            Assert.Equal(JsonSerializer.Serialize(expectedResult), JsonSerializer.Serialize(actualResult));
        }

        [Fact]
        public async void AddAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = BorrowingsHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            var claims = new List<Claim>()
            {
                new Claim("UserId", "2")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            // Act
            var ids = new List<int> { 6 };
            await handler.AddAsync(new BorrowingAddDto { UserConsoleIds = ids }, claimsPrincipal);
            var actualResult = dbMock.Borrowings.Last();
            var updatedUserConsoles = dbMock.UserConsoles.Last();
            // Assert
            Assert.Equal(2, actualResult.UserId);
            Assert.Equal(BorrowingStatus.PENDING, actualResult.Status);
            Assert.Equal(updatedUserConsoles.BorrowingId, actualResult.Id);
        }


        [Fact]
        public async void UpdateAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = BorrowingsHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act
            var actualResult = 1;
            var expectedResult = 0;
            // Assert
            Assert.Equal(JsonSerializer.Serialize(expectedResult), JsonSerializer.Serialize(actualResult));
        }


        [Fact]
        public async void UpdateStatusAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = BorrowingsHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act
            await handler.UpdateStatusAsync(new BorrowingUpdateStatusDto{ Id = 2, BorrowingStatus = BorrowingStatus.ACTIVE });
            var borrowing = dbMock.Borrowings.Where(x => x.Id == 2).First();
            // Assert
            Assert.Equal(BorrowingStatus.ACTIVE, borrowing.Status);
        }


        [Fact]
        public async void DeleteAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = BorrowingsHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act
            var actualResult = 1;
            var expectedResult = 0;
            // Assert
            Assert.Equal(JsonSerializer.Serialize(expectedResult), JsonSerializer.Serialize(actualResult));
        }


        [Fact]
        public async void CanDeleteTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = BorrowingsHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act
            var actualResult = 1;
            var expectedResult = 0;
            // Assert
            Assert.Equal(JsonSerializer.Serialize(expectedResult), JsonSerializer.Serialize(actualResult));
        }

    }
}