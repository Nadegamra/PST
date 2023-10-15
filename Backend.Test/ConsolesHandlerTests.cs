using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Backend.Data.Views.Console;
using Backend.Data.Views.Image;
using Backend.Test.Mocks.Handlers;

namespace Backend.Test
{
    public class ConsolesHandlerTests
    {
        [Fact]
        public async void GetConsolesAsyncTest()
        {
            // Act
            var dbMock = new DbContextMock();
            var handler = ConsolesHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Arrange
            var actualResult = await handler.GetConsolesAsync();
            var expectedResult = mapper.Map<List<Data.Models.Console>, List<ConsoleGetDto>>(dbMock.Consoles);
            // Assert
            Assert.Equal(JsonSerializer.Serialize(actualResult), JsonSerializer.Serialize(expectedResult));
        }
        [Fact]
        public async void GetConsoleAsyncTest()
        {
            // Act
            var dbMock = new DbContextMock();
            var handler = ConsolesHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Arrange
            var actualResult = await handler.GetConsoleAsync(2);
            var expectedResult = mapper.Map<Data.Models.Console, ConsoleGetDto>(dbMock.Consoles.First((x) => x.Id == 2));
            // Assert
            Assert.Equal(JsonSerializer.Serialize(actualResult), JsonSerializer.Serialize(expectedResult));
        }
        [Fact]
        public async void AddConsoleAsyncTest()
        {
            // Act
            var dbMock = new DbContextMock();
            var handler = ConsolesHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Arrange
            var images = new List<ImageAddDto>() { new ImageAddDto { Name = "name", Description = "description", Stream = Convert.ToBase64String(Encoding.UTF8.GetBytes("stream")) } };
            await handler.AddConsoleAsync(new ConsoleAddDto { Name = "addedName", DailyPrice = 6.99m, Description = "description123", Images = images });
            var actualResult = dbMock.Consoles.Last();
            var addedImages = new List<Image> { dbMock.Images.Last() };
            // Assert
            Assert.Equal("addedName", actualResult.Name);
            Assert.Equal(6.99m, actualResult.DailyPrice);
            Assert.Equal("description123", actualResult.Description);
            Assert.Equal(JsonSerializer.Serialize(actualResult.Images), JsonSerializer.Serialize(addedImages));
        }
        [Fact]
        public async void UpdateConsoleAsyncTest()
        {
            // Act
            var dbMock = new DbContextMock();
            var handler = ConsolesHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Arrange
            await handler.UpdateConsoleAsync(new ConsoleUpdateDto { Id = 1, DailyPrice = 4.2m, Description = "newDescription", Name = "newName", Images = new List<ImageUpdateDto>() });
            var actualResult = dbMock.Consoles.Where(x => x.Id == 1).First();
            // Assert
            Assert.Equal(4.2m, actualResult.DailyPrice);
            Assert.Equal("newName", actualResult.Name);
            Assert.Equal("newDescription", actualResult.Description);
        }
        [Fact]
        public async void RemoveConsoleAsyncTest()
        {
            // Act
            var dbMock = new DbContextMock();
            var handler = ConsolesHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Arrange
            await handler.RemoveConsoleAsync(4);
            var deletedConsole = dbMock.Consoles.FirstOrDefault(x => x.Id == 4);
            // Assert
            Assert.Null(deletedConsole);
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await handler.RemoveConsoleAsync(3));
        }
        [Fact]
        public async void CanDelete()
        {
            // Act
            var dbMock = new DbContextMock();
            var handler = ConsolesHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Arrange
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