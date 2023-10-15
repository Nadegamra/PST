using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Backend.Test.Mocks
{
    public class UsersHandlerMock
    {
        public static SmtpConfig ConfigData = new SmtpConfig { Username = "email@example.com", TestEmail = "tester@example.com", Password = "Password" };
        public static UsersHandler GetMock(DbContextMock mock)
        {
            var dbMock = mock.DbMock;
            var userManagerMock = UserManagerMock.MockUserManager(mock.Users);
            var configMock = new Mock<IOptions<SmtpConfig>>();
            configMock.Setup(x => x.Value).Returns(ConfigData);
            var handler = new UsersHandler(userManagerMock.Object, dbMock.Object, configMock.Object);
            return handler;
        }
    }
}