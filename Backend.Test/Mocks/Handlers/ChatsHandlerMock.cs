using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Backend.Test.Mocks.Handlers
{
    public class ChatsHandlerMock
    {
        public static ChatsHandler GetMock(DbContextMock dbMock)
        {
            IMapper mapper = AutomapperMock.GetMock();
            FilesHandler filesHandler = FilesHandlerMock.GetMock(dbMock);
            var userManager = UserManagerMock.MockUserManager(dbMock.Users, dbMock.UserRoles, dbMock.Roles);
            var handler = new ChatsHandler(dbMock.DbMock.Object, userManager.Object, mapper, filesHandler);
            return handler;
        }
    }
}