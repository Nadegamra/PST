using AutoMapper;

namespace Backend.Test.Mocks.Handlers
{
    public class BorrowingsHandlerMock
    {
        public static BorrowingsHandler GetMock(DbContextMock mock)
        {
            var dbMock = mock.DbMock;
            IMapper mapper = AutomapperMock.GetMock();
            var userManagerMock = UserManagerMock.MockUserManager(mock.Users, mock.UserRoles, mock.Roles);
            var userConsolesHandler = UserConsolesHandlerMock.GetMock(mock);
            var handler = new BorrowingsHandler(mapper, dbMock.Object, userManagerMock.Object, userConsolesHandler);
            return handler;
        }
    }
}