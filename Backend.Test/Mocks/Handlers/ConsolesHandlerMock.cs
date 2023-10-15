using AutoMapper;

namespace Backend.Test.Mocks.Handlers
{
    public class ConsolesHandlerMock
    {
        public static ConsolesHandler GetMock(DbContextMock dbMock)
        {
            IMapper mapper = AutomapperMock.GetMock();
            FilesHandler filesHandler = FilesHandlerMock.GetMock(dbMock);
            var handler = new ConsolesHandler(dbMock.DbMock.Object, filesHandler, mapper);
            return handler;
        }
    }
}