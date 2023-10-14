using Backend.Data;
using Microsoft.AspNetCore.Identity;
using Moq.EntityFrameworkCore;

namespace Backend.Test.Mocks
{
    public class DbContextMock
    {
        public List<User> Users = new List<User>(MockData.Users.Select(x => new User
        {
            Id = x.Id,
            AccessFailedCount = x.AccessFailedCount,
            City = x.City,
            CompanyCode = x.CompanyCode,
            CompanyName = x.CompanyName,
            ConcurrencyStamp = x.ConcurrencyStamp,
            Country = x.Country,
            County = x.County,
            Email = x.Email,
            EmailConfirmed = x.EmailConfirmed,
            FirstName = x.FirstName,
            IsCompany = x.IsCompany,
            LastName = x.LastName,
            LockoutEnabled = x.LockoutEnabled,
            LockoutEnd = x.LockoutEnd,
            NormalizedEmail = x.NormalizedEmail,
            NormalizedUserName = x.NormalizedUserName,
            PasswordHash = x.PasswordHash,
            PhoneNumber = x.PhoneNumber,
            PhoneNumberConfirmed = x.PhoneNumberConfirmed,
            PostalCode = x.PostalCode,
            SecurityStamp = x.SecurityStamp,
            StreetAddress = x.StreetAddress,
            TwoFactorEnabled = x.TwoFactorEnabled,
            UserName = x.UserName
        }));
        public List<IdentityUserRole<int>> UserRoles = MockData.UserRoles;
        public List<IdentityRole<int>> Roles = MockData.Roles;
        public List<RegistrationRequest> RegistrationRequests = MockData.RegistrationRequests;
        public List<PasswordResetToken> PasswordResetTokens = MockData.PasswordResetTokens;
        public List<EmailConfirmationToken> EmailConfirmationTokens = MockData.EmailConfirmationTokens;
        public List<EmailChangeToken> EmailChangeTokens = MockData.EmailChangeTokens;
        public List<Conversation> Conversations = MockData.Conversations;
        public List<Message> Messages = MockData.Messages;
        public List<MessageFile> MessageFiles = MockData.MessageFiles;
        public List<Data.Models.Console> Consoles = MockData.Consoles;
        public List<Borrowing> Borrowings = MockData.Borrowings;
        public List<UserConsole> UserConsoles = MockData.UserConsoles;
        public List<Image> Images = MockData.Images;

        public Mock<AppDbContext> DbMock { get; set; } = new Mock<AppDbContext>();

        public DbContextMock()
        {
            DbMock.Setup(x => x.Users).ReturnsDbSet(Users);
            DbMock.Setup(x => x.UserRoles).ReturnsDbSet(UserRoles);
            DbMock.Setup(x => x.Roles).ReturnsDbSet(Roles);
            DbMock.Setup(x => x.RegistrationRequests).ReturnsDbSet(RegistrationRequests);
            DbMock.Setup(x => x.PasswordResetTokens).ReturnsDbSet(PasswordResetTokens);
            DbMock.Setup(x => x.EmailConfirmationTokens).ReturnsDbSet(EmailConfirmationTokens);
            DbMock.Setup(x => x.EmailChangeTokens).ReturnsDbSet(EmailChangeTokens);
            DbMock.Setup(x => x.Conversations).ReturnsDbSet(Conversations);
            DbMock.Setup(x => x.Messages).ReturnsDbSet(Messages);
            DbMock.Setup(x => x.MessageFiles).ReturnsDbSet(MessageFiles);
            DbMock.Setup(x => x.Consoles).ReturnsDbSet(Consoles);
            DbMock.Setup(x => x.Borrowings).ReturnsDbSet(Borrowings);
            DbMock.Setup(x => x.UserConsoles).ReturnsDbSet(UserConsoles);
            DbMock.Setup(x => x.Images).ReturnsDbSet(Images);


            DbMock.Setup(x => x.PasswordResetTokens.Add(It.IsAny<PasswordResetToken>())).Returns((PasswordResetToken token) =>
            {
                token.Id = PasswordResetTokens.Last().Id + 1;
                PasswordResetTokens.Add(token);
                return null;
            });
        }
    }
}