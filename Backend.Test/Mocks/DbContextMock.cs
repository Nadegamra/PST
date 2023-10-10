using Backend.Data;
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
            dbMock.Setup(x => x.RegistrationRequests).ReturnsDbSet(MockData.RegistrationRequests);
            dbMock.Setup(x => x.PasswordResetTokens).ReturnsDbSet(MockData.PasswordResetTokens);
            dbMock.Setup(x => x.EmailConfirmationTokens).ReturnsDbSet(MockData.EmailConfirmationTokens);
            dbMock.Setup(x => x.EmailChangeTokens).ReturnsDbSet(MockData.EmailChangeTokens);
            dbMock.Setup(x => x.Conversations).ReturnsDbSet(MockData.Conversations);
            dbMock.Setup(x => x.Messages).ReturnsDbSet(MockData.Messages);
            dbMock.Setup(x => x.MessageFiles).ReturnsDbSet(MockData.MessageFiles);
            dbMock.Setup(x => x.Consoles).ReturnsDbSet(MockData.Consoles);
            dbMock.Setup(x => x.Borrowings).ReturnsDbSet(MockData.Borrowings);
            dbMock.Setup(x => x.UserConsoles).ReturnsDbSet(MockData.UserConsoles);
            dbMock.Setup(x => x.Images).ReturnsDbSet(MockData.Images);

            return dbMock;
        }
    }
}