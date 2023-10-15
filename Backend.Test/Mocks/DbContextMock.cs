using System.Text.Json;
using Backend.Data;
using Microsoft.AspNetCore.Identity;
using Moq.EntityFrameworkCore;

namespace Backend.Test.Mocks
{
    public class DbContextMock
    {
        public List<User> Users = JsonSerializer.Deserialize(JsonSerializer.Serialize(MockData.Users), typeof(List<User>)) as List<User>;
        public List<IdentityUserRole<int>> UserRoles = JsonSerializer.Deserialize(JsonSerializer.Serialize(MockData.UserRoles), typeof(List<IdentityUserRole<int>>)) as List<IdentityUserRole<int>>;
        public List<IdentityRole<int>> Roles = JsonSerializer.Deserialize(JsonSerializer.Serialize(MockData.Roles), typeof(List<IdentityRole<int>>)) as List<IdentityRole<int>>;
        public List<RegistrationRequest> RegistrationRequests = JsonSerializer.Deserialize(JsonSerializer.Serialize(MockData.RegistrationRequests), typeof(List<RegistrationRequest>)) as List<RegistrationRequest>;
        public List<PasswordResetToken> PasswordResetTokens = JsonSerializer.Deserialize(JsonSerializer.Serialize(MockData.PasswordResetTokens), typeof(List<PasswordResetToken>)) as List<PasswordResetToken>;
        public List<EmailConfirmationToken> EmailConfirmationTokens = JsonSerializer.Deserialize(JsonSerializer.Serialize(MockData.EmailConfirmationTokens), typeof(List<EmailConfirmationToken>)) as List<EmailConfirmationToken>;
        public List<EmailChangeToken> EmailChangeTokens = JsonSerializer.Deserialize(JsonSerializer.Serialize(MockData.EmailChangeTokens), typeof(List<EmailChangeToken>)) as List<EmailChangeToken>;
        public List<Conversation> Conversations = JsonSerializer.Deserialize(JsonSerializer.Serialize(MockData.Conversations), typeof(List<Conversation>)) as List<Conversation>;
        public List<Message> Messages = JsonSerializer.Deserialize(JsonSerializer.Serialize(MockData.Messages), typeof(List<Message>)) as List<Message>;
        public List<MessageFile> MessageFiles = JsonSerializer.Deserialize(JsonSerializer.Serialize(MockData.MessageFiles), typeof(List<MessageFile>)) as List<MessageFile>;
        public List<Data.Models.Console> Consoles = JsonSerializer.Deserialize(JsonSerializer.Serialize(MockData.Consoles), typeof(List<Data.Models.Console>)) as List<Data.Models.Console>;
        public List<Borrowing> Borrowings = JsonSerializer.Deserialize(JsonSerializer.Serialize(MockData.Borrowings), typeof(List<Borrowing>)) as List<Borrowing>;
        public List<UserConsole> UserConsoles = JsonSerializer.Deserialize(JsonSerializer.Serialize(MockData.UserConsoles), typeof(List<UserConsole>)) as List<UserConsole>;
        public List<Image> Images = JsonSerializer.Deserialize(JsonSerializer.Serialize(MockData.Images), typeof(List<Image>)) as List<Image>;

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
            DbMock.Setup(x => x.EmailConfirmationTokens.Add(It.IsAny<EmailConfirmationToken>())).Returns((EmailConfirmationToken token) =>
            {
                token.Id = EmailConfirmationTokens.Last().Id + 1;
                EmailConfirmationTokens.Add(token);
                return null;
            });
            DbMock.Setup(x => x.EmailChangeTokens.Add(It.IsAny<EmailChangeToken>())).Returns((EmailChangeToken token) =>
            {
                token.Id = EmailChangeTokens.Last().Id + 1;
                EmailChangeTokens.Add(token);
                return null;
            });
        }
    }
}