using System.Text.Json;
using Backend.Data;
using Backend.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
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
            DbMock.Setup(x => x.Images.Add(It.IsAny<Image>())).Returns((Image image) =>
            {
                image.Id = Images.Last().Id + 1;
                Images.Add(image);
                if (image.UserConsoleId != null)
                {
                    int idx = UserConsoles.FindIndex(x => x.Id == image.UserConsoleId);
                    UserConsoles[idx].Images.Add(image);
                }
                else if (image.ConsoleId != null)
                {
                    int idx = Consoles.FindIndex(x => x.Id == image.ConsoleId);
                    Consoles[idx].Images.Add(image);
                }
                return null;
            });
            DbMock.Setup(x => x.Images.Remove(It.IsAny<Image>())).Returns((Image image) =>
            {
                int idx = Images.FindIndex(x => x.Id == image.Id);
                Images.RemoveAt(idx);
                return null;
            });
            DbMock.Setup(x => x.MessageFiles.Add(It.IsAny<MessageFile>())).Returns((MessageFile messageFile) =>
            {
                messageFile.Id = MessageFiles.Last().Id + 1;
                MessageFiles.Add(messageFile);
                return null;
            });
            DbMock.Setup(x => x.UserConsoles.Add(It.IsAny<UserConsole>())).Returns((UserConsole userConsole) =>
            {
                userConsole.Id = UserConsoles.Last().Id + 1;
                userConsole.Images = new List<Image>();
                UserConsoles.Add(userConsole);

                return new EntityEntry<UserConsole>
                (new InternalEntityEntry(
                    new Mock<IStateManager>().Object,
                    new RuntimeEntityType("UserConsole", typeof(UserConsole), false, null, null, null, Microsoft.EntityFrameworkCore.ChangeTrackingStrategy.Snapshot, null, false, null),
                    userConsole
                    ));
            });
            DbMock.Setup(x => x.UserConsoles.Remove(It.IsAny<UserConsole>())).Returns((UserConsole userConsole) =>
            {
                int idx = UserConsoles.FindIndex(x => x.Id == userConsole.Id);
                UserConsoles.RemoveAt(idx);
                return null;
            });
            DbMock.Setup(x => x.Consoles.Add(It.IsAny<Data.Models.Console>())).Returns((Data.Models.Console console) =>
            {
                console.Id = Consoles.Last().Id + 1;
                console.Images = new List<Image>();
                Consoles.Add(console);

                return new EntityEntry<Data.Models.Console>
                (new InternalEntityEntry(
                    new Mock<IStateManager>().Object,
                    new RuntimeEntityType("Console", typeof(Data.Models.Console), false, null, null, null, Microsoft.EntityFrameworkCore.ChangeTrackingStrategy.Snapshot, null, false, null),
                    console
                    ));
            });
            DbMock.Setup(x => x.Consoles.Remove(It.IsAny<Data.Models.Console>())).Returns((Data.Models.Console console) =>
            {
                int idx = Consoles.FindIndex(x => x.Id == console.Id);
                Consoles.RemoveAt(idx);
                return null;
            });
            DbMock.Setup(x => x.Conversations.Add(It.IsAny<Conversation>())).Returns((Conversation conversation) =>
            {
                conversation.Id = Conversations.Last().Id + 1;
                Conversations.Add(conversation);

                return new EntityEntry<Conversation>
                (new InternalEntityEntry(
                    new Mock<IStateManager>().Object,
                    new RuntimeEntityType("Conversation", typeof(Conversation), false, null, null, null, Microsoft.EntityFrameworkCore.ChangeTrackingStrategy.Snapshot, null, false, null),
                    conversation
                    ));
            });
            DbMock.Setup(x => x.Messages.Add(It.IsAny<Message>())).Returns((Message message) =>
            {
                message.Id = Messages.Last().Id + 1;
                Messages.Add(message);
                return null;
            });

            DbMock.Setup(x => x.Borrowings.Add(It.IsAny<Borrowing>())).Returns((Borrowing borrowing) =>
            {
                borrowing.Id = Borrowings.Last().Id + 1;
                Borrowings.Add(borrowing);

                    return new EntityEntry<Borrowing>
                (new InternalEntityEntry(
                    new Mock<IStateManager>().Object,
                    new RuntimeEntityType("Borrowing", typeof(Borrowing), false, null, null, null, Microsoft.EntityFrameworkCore.ChangeTrackingStrategy.Snapshot, null, false, null),
                    borrowing
                    ));
            });
            DbMock.Setup(x => x.Borrowings.Remove(It.IsAny<Borrowing>())).Returns((Borrowing borrowing) =>
            {
                int idx = Borrowings.FindIndex(x => x.Id == borrowing.Id);
                Borrowings.RemoveAt(idx);
                return null;
            });
            DbMock.Setup(x => x.Conversations.Remove(It.IsAny<Conversation>())).Returns((Conversation conversation) =>
            {
                int idx = Conversations.FindIndex(x => x.Id == conversation.Id);
                Conversations.RemoveAt(idx);
               return null;
            });
          
            DbMock.Setup(x => x.RegistrationRequests.Add(It.IsAny<RegistrationRequest>())).Returns((RegistrationRequest registrationRequest) =>
            {
                registrationRequest.Id = RegistrationRequests.Last().Id + 1;
                RegistrationRequests.Add(registrationRequest);

                return new EntityEntry<RegistrationRequest>
                (new InternalEntityEntry(
                    new Mock<IStateManager>().Object,
                    new RuntimeEntityType("RegistrationRequest", typeof(RegistrationRequest), false, null, null, null, Microsoft.EntityFrameworkCore.ChangeTrackingStrategy.Snapshot, null, false, null),
                    registrationRequest
                    ));
            });
            DbMock.Setup(x => x.RegistrationRequests.Remove(It.IsAny<RegistrationRequest>())).Returns((RegistrationRequest registrationRequest) =>
            {
                int idx = RegistrationRequests.FindIndex(x => x.Id == registrationRequest.Id);
                RegistrationRequests.RemoveAt(idx);
               return null;
            });
        }
        delegate UserConsole AddUserConsole(UserConsole console);
    }
}