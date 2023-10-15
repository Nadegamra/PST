using System.Text;
using System.Text.Json;
using AutoMapper;
using Backend.Data.Views.Image;
using Microsoft.Extensions.Options;

namespace Backend.Test
{
    public class FilesHandlerTests
    {
        private static CloudinaryConfig _configData = new CloudinaryConfig { Cloud = "drzqsbvky", ApiKey = "597561875277626", ApiSecret = "Y8D_hkUkMl0uWUZwy3lbkoJiF0g" };
        [Fact]
        public async void GetConsoleImagesAsyncTest()
        {
            // Arrange
            var mock = new DbContextMock();
            var dbMock = mock.DbMock;
            var mapperProfile = new MappingProfile();
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));
            IMapper mapper = new Mapper(mapperConfig);
            var configMock = new Mock<IOptions<CloudinaryConfig>>();
            configMock.Setup(x => x.Value).Returns(_configData);
            var handler = new FilesHandler(dbMock.Object, mapper, configMock.Object, true);
            // Act
            var actualResult = await handler.GetConsoleImagesAsync(1);
            var expectedResult = mapper.Map<List<Image>, List<ImageGetDto>>(mock.Images.Where(x => x.ConsoleId == 1).ToList());
            // Assert
            Assert.Equal(JsonSerializer.Serialize(actualResult), JsonSerializer.Serialize(expectedResult));
        }
        [Fact]
        public async void GetUserConsolesImagesAsyncTest()
        {
            // Arrange
            var mock = new DbContextMock();
            var dbMock = mock.DbMock;
            var mapperProfile = new MappingProfile();
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));
            IMapper mapper = new Mapper(mapperConfig);
            var configMock = new Mock<IOptions<CloudinaryConfig>>();
            configMock.Setup(x => x.Value).Returns(_configData);
            var handler = new FilesHandler(dbMock.Object, mapper, configMock.Object, true);
            // Act
            var actualResult = await handler.GetUserConsoleImagesAsync(2);
            var expectedResult = mapper.Map<List<Image>, List<ImageGetDto>>(mock.Images.Where(x => x.UserConsoleId == 2).ToList());
            // Assert
            Assert.Equal(JsonSerializer.Serialize(actualResult), JsonSerializer.Serialize(expectedResult));
        }
        [Fact]
        public async void GetImageAsyncTest()
        {
            // Arrange
            var mock = new DbContextMock();
            var dbMock = mock.DbMock;
            var mapperProfile = new MappingProfile();
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));
            IMapper mapper = new Mapper(mapperConfig);
            var configMock = new Mock<IOptions<CloudinaryConfig>>();
            configMock.Setup(x => x.Value).Returns(_configData);
            var handler = new FilesHandler(dbMock.Object, mapper, configMock.Object, true);
            // Act
            var actualResult = await handler.GetImageAsync(3);
            var expectedResult = mapper.Map<Image, ImageGetDto>(mock.Images.Where(x => x.Id == 3).First());
            // Assert
            Assert.Equal(JsonSerializer.Serialize(actualResult), JsonSerializer.Serialize(expectedResult));
        }
        [Fact]
        public async void AddImageAsyncTest()
        {
            // Arrange
            var mock = new DbContextMock();
            var dbMock = mock.DbMock;
            var mapperProfile = new MappingProfile();
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));
            IMapper mapper = new Mapper(mapperConfig);
            var configMock = new Mock<IOptions<CloudinaryConfig>>();
            configMock.Setup(x => x.Value).Returns(_configData);
            var handler = new FilesHandler(dbMock.Object, mapper, configMock.Object, true);
            // Act
            await handler.AddImageAsync(new ImageAddDto() { ConsoleId = 1, Description = "desc", Name = "name", Stream = System.Convert.ToBase64String(Encoding.UTF8.GetBytes("stream")) });
            var uploadedImage = mock.Images.Last();
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
            var mock = new DbContextMock();
            var dbMock = mock.DbMock;
            var mapperProfile = new MappingProfile();
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));
            IMapper mapper = new Mapper(mapperConfig);
            var configMock = new Mock<IOptions<CloudinaryConfig>>();
            configMock.Setup(x => x.Value).Returns(_configData);
            var handler = new FilesHandler(dbMock.Object, mapper, configMock.Object, true);
            // Act

            // Assert
            Assert.Equal(1, 0);
        }
        [Fact]
        public async void RemoveImageAsyncTest()
        {
            // Arrange
            var mock = new DbContextMock();
            var dbMock = mock.DbMock;
            var mapperProfile = new MappingProfile();
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));
            IMapper mapper = new Mapper(mapperConfig);
            var configMock = new Mock<IOptions<CloudinaryConfig>>();
            configMock.Setup(x => x.Value).Returns(_configData);
            var handler = new FilesHandler(dbMock.Object, mapper, configMock.Object, true);
            // Act

            // Assert
            Assert.Equal(1, 0);
        }
        [Fact]
        public async void AddMessageFileAsyncTest()
        {
            // Arrange
            var mock = new DbContextMock();
            var dbMock = mock.DbMock;
            var mapperProfile = new MappingProfile();
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(mapperProfile));
            IMapper mapper = new Mapper(mapperConfig);
            var configMock = new Mock<IOptions<CloudinaryConfig>>();
            configMock.Setup(x => x.Value).Returns(_configData);
            var handler = new FilesHandler(dbMock.Object, mapper, configMock.Object, true);
            // Act

            // Assert
            Assert.Equal(1, 0);
        }

    }
}