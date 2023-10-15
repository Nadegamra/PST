using AutoMapper;
using Microsoft.Extensions.Options;

namespace Backend.Test.Mocks
{
    public class FilesHandlerMock
    {
        public static CloudinaryConfig ConfigData = new CloudinaryConfig { Cloud = "drzqsbvky", ApiKey = "597561875277626", ApiSecret = "Y8D_hkUkMl0uWUZwy3lbkoJiF0g" };
        public static FilesHandler GetMock(DbContextMock mock)
        {
            var dbMock = mock.DbMock;
            IMapper mapper = AutomapperMock.GetMock();
            var configMock = new Mock<IOptions<CloudinaryConfig>>();
            configMock.Setup(x => x.Value).Returns(ConfigData);
            var handler = new FilesHandler(dbMock.Object, mapper, configMock.Object, true);
            return handler;
        }
    }
}