using System.Text;
using System.Text.Json;
using Backend.Data.Views.Console;
using Backend.Data.Views.BorrowedConsole;
using Backend.Data.Views.Image;
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
            // Act
            var actualResult = 1;
            var expectedResult = 0;
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
            var actualResult = 1;
            var expectedResult = 0;
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
            // Act
            var actualResult = 1;
            var expectedResult = 0;
            // Assert
            Assert.Equal(JsonSerializer.Serialize(expectedResult), JsonSerializer.Serialize(actualResult));
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
            var actualResult = 1;
            var expectedResult = 0;
            // Assert
            Assert.Equal(JsonSerializer.Serialize(expectedResult), JsonSerializer.Serialize(actualResult));
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