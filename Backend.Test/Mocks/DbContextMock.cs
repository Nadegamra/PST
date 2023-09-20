using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data;
using Moq;
using Moq.EntityFrameworkCore;

namespace Backend.Test.Mocks
{
    public class DbContextMock
    {
        public static Mock<AppDbContext> MockDbContext()
        {
            var dbMock = new Mock<AppDbContext>();
            dbMock.Setup(x => x.Users).ReturnsDbSet(MockData.Users);
            dbMock.Setup(x => x.UserRoles).ReturnsDbSet(MockData.UserRoles);
            dbMock.Setup(x => x.Roles).ReturnsDbSet(MockData.Roles);

            return dbMock;
        }
    }
}