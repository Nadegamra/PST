using System.Text;
using System.Text.Json;
using Backend.Data.Views.Console;
using Backend.Data.Views.BorrowedConsole;
using Backend.Data.Views.Image;
using Backend.Test.Mocks.Handlers;
using System.Security.Claims;
using Backend.Data.Views.UserConsole;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

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
                new Claim("UserId", "3")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var ids = new List<int> { 14, 8 };
            // Act
            await handler.AddAsync(new BorrowingAddDto { UserConsoleIds = ids }, claimsPrincipal);
            var actualResult = dbMock.Borrowings.Last();
            // Assert
            Assert.Equal(3, actualResult.UserId);
            Assert.Equal(BorrowingStatus.PENDING, actualResult.Status);
            foreach (int id in ids)
            {
                var updatedUserConsoles = dbMock.UserConsoles.Where(x => x.Id == id).First();
                Assert.Equal(updatedUserConsoles.BorrowingId, actualResult.Id);
            }
        }


        [Fact]
        public async void UpdateAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = BorrowingsHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            var claims = new List<Claim>()
            {
                new Claim("UserId", "1")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var UserConsoleIds = new List<int> { 14, 8 };
            // Act
            await handler.UpdateAsync(new BorrowingUpdateDto { Id = 2, UserConsoleIds = UserConsoleIds }, claimsPrincipal);
            var actualResult = dbMock.UserConsoles.Where(x => x.BorrowingId == 2).ToList();
            // Assert
            foreach(UserConsole ar in actualResult)
                Assert.Equal(UserConsoleStatus.AT_PLATFORM, ar.ConsoleStatus);

            foreach (int id in UserConsoleIds)
            {
                var updatedUserConsole = dbMock.UserConsoles.Where(x => x.Id == id).First();
                Assert.Equal(UserConsoleStatus.AT_LENDER, updatedUserConsole.ConsoleStatus);
            }
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
            var updatedUserConsoles = dbMock.UserConsoles.Where(x => x.BorrowingId == 2).ToList();
            // Assert
            Assert.Equal(BorrowingStatus.ACTIVE, borrowing.Status);
            foreach (UserConsole userConsole in updatedUserConsoles)
                Assert.Equal(UserConsoleStatus.AT_LENDER, userConsole.ConsoleStatus);
        }


        [Fact]
        public async void DeleteAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = BorrowingsHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act
            await handler.DeleteAsync(3);
            var deletedBorrowing = dbMock.Borrowings.FirstOrDefault(x => x.Id == 3);
            var deletedConversation = dbMock.Conversations.FirstOrDefault(x => x.BorrowingId == 3);
            // Assert
            Assert.Null(deletedBorrowing);
            Assert.Null(deletedConversation);
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await handler.DeleteAsync(3));
        }


        [Fact]
        public async void CanDeleteTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = BorrowingsHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act
            var res1 = handler.CanDelete(2);
            var res2 = handler.CanDelete(3);
            var res3 = handler.CanDelete(4);
            // Assert
            Assert.False(res1);
            Assert.False(res2);
            Assert.True(res3);
        }

    }
}