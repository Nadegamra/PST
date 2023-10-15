using AutoMapper;

namespace Backend.Test.Mocks.Handlers
{
    public class UserConsolesHandlerMock
    {
        public static UserConsolesHandler GetMock(DbContextMock dbMock)
        {
            IMapper mapper = AutomapperMock.GetMock();
            FilesHandler filesHandler = FilesHandlerMock.GetMock(dbMock);
            var userManager = UserManagerMock.MockUserManager(dbMock.Users, dbMock.UserRoles, dbMock.Roles);
            var handler = new UserConsolesHandler(mapper, filesHandler, dbMock.DbMock.Object, userManager.Object);
            return handler;
        }
    }
}