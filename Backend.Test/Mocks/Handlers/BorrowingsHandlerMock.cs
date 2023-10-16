using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Backend.Test.Mocks.Handlers
{
    public class BorrowingsHandlerMock
    {
        public static BorrowingsHandler GetMock(DbContextMock dbMock)
        {
            IMapper mapper = AutomapperMock.GetMock();
            UserConsolesHandler userConsolesHandler = UserConsolesHandlerMock.GetMock(dbMock);
            var userManager = UserManagerMock.MockUserManager(dbMock.Users, dbMock.UserRoles, dbMock.Roles);
            var handler = new BorrowingsHandler(mapper, dbMock.DbMock.Object, userManager.Object, userConsolesHandler);
            return handler;
        }
    }
}