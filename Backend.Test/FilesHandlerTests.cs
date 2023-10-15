using System.Text;
using System.Text.Json;
using AutoMapper;
using Backend.Data.Views.Image;
using Microsoft.Extensions.Options;

namespace Backend.Test
{
    public class FilesHandlerTests
    {
        [Fact]
        public async void GetConsoleImagesAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var mapper = AutomapperMock.GetMock();
            var handler = FilesHandlerMock.GetMock(dbMock);
            // Act
            var actualResult = await handler.GetConsoleImagesAsync(1);
            var expectedResult = mapper.Map<List<Image>, List<ImageGetDto>>(dbMock.Images.Where(x => x.ConsoleId == 1).ToList());
            // Assert
            Assert.Equal(JsonSerializer.Serialize(actualResult), JsonSerializer.Serialize(expectedResult));
        }
        [Fact]
        public async void GetUserConsolesImagesAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var mapper = AutomapperMock.GetMock();
            var handler = FilesHandlerMock.GetMock(dbMock);
            // Act
            var actualResult = await handler.GetUserConsoleImagesAsync(2);
            var expectedResult = mapper.Map<List<Image>, List<ImageGetDto>>(dbMock.Images.Where(x => x.UserConsoleId == 2).ToList());
            // Assert
            Assert.Equal(JsonSerializer.Serialize(actualResult), JsonSerializer.Serialize(expectedResult));
        }
        [Fact]
        public async void GetImageAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var mapper = AutomapperMock.GetMock();
            var handler = FilesHandlerMock.GetMock(dbMock);
            // Act
            var actualResult = await handler.GetImageAsync(3);
            var expectedResult = mapper.Map<Image, ImageGetDto>(dbMock.Images.Where(x => x.Id == 3).First());
            // Assert
            Assert.Equal(JsonSerializer.Serialize(actualResult), JsonSerializer.Serialize(expectedResult));
        }
        [Fact]
        public async void AddImageAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var mapper = AutomapperMock.GetMock();
            var handler = FilesHandlerMock.GetMock(dbMock);
            // Act
            await handler.AddImageAsync(new ImageAddDto() { ConsoleId = 1, Description = "desc", Name = "name", Stream = System.Convert.ToBase64String(Encoding.UTF8.GetBytes("stream")) });
            var uploadedImage = dbMock.Images.Last();
            // Assert
            Assert.Equal(1, uploadedImage.ConsoleId);
            Assert.Equal("desc", uploadedImage.Description);
            Assert.Equal("name", uploadedImage.Name);
            Assert.Equal("ImageId", uploadedImage.Path);
        }
        [Fact]
        public async void UpdateImageAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var mapper = AutomapperMock.GetMock();
            var handler = FilesHandlerMock.GetMock(dbMock);
            // Act
            await handler.UpdateImageAsync(new ImageUpdateDto { Id = 1, Name = "NewName", Description = "NewDescription" });
            var updatedImage = dbMock.Images.Where(x => x.Id == 1).First();
            // Assert
            Assert.Equal("NewName", updatedImage.Name);
            Assert.Equal("NewDescription", updatedImage.Description);
        }
        [Fact]
        public async void RemoveImageAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var mapper = AutomapperMock.GetMock();
            var handler = FilesHandlerMock.GetMock(dbMock);
            // Act
            await handler.RemoveImageAsync(2);
            var deletedImage = dbMock.Images.Where(x => x.Id == 2).FirstOrDefault();
            // Assert
            Assert.Null(deletedImage);
        }
        [Fact]
        public async void AddMessageFileAsyncTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var mapper = AutomapperMock.GetMock();
            var handler = FilesHandlerMock.GetMock(dbMock);
            // Act
            await handler.AddMessageFileAsync(new Data.Views.MessageFile.MessageFileAddDto { MessageId = 1, Description = "desc", Name = "name", Stream = System.Convert.ToBase64String(Encoding.UTF8.GetBytes("stream")) });
            var messageFile = dbMock.MessageFiles.Last();
            // Assert
            Assert.Equal(1, messageFile.MessageId);
            Assert.Equal("desc", messageFile.Description);
            Assert.Equal("name", messageFile.Name);
        }

    }
}