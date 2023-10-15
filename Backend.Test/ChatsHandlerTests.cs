using System.Security.Claims;
using System.Text.Json;
using Backend.Data.Views.Chat;
using Backend.Data.Views.Message;
using Backend.Data.Views.MessageFile;
using Backend.Test.Mocks.Handlers;

namespace Backend.Test
{
    public class ChatsHandlerTests
    {
        [Fact]
        public async void GetAllConversationsTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = ChatsHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act
            var actualResult = await handler.GetAllConversations();
            var expectedResult = mapper.Map<List<Conversation>, List<ConversationGetDto>>(dbMock.Conversations.ToList());
            // Assert
            Assert.Equal(JsonSerializer.Serialize(actualResult), JsonSerializer.Serialize(expectedResult));
        }
        [Fact]
        public async void GetLenderConversationsTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = ChatsHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act
            var claims = new List<Claim>()
            {
                new Claim("UserId", "2")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var actualResult = await handler.GetLenderConversations(claimsPrincipal);
            var userConsoleIds = dbMock.UserConsoles.Where(x => x.UserId == 2).Select(x => x.Id).ToList();
            var expectedResult = mapper.Map<List<Conversation>, List<ConversationGetDto>>(dbMock.Conversations.Where(x => x.UserConsoleId is not null && userConsoleIds.Contains(x.UserConsoleId ?? -1)).ToList());
            // Assert
            Assert.Equal(JsonSerializer.Serialize(actualResult), JsonSerializer.Serialize(expectedResult));
        }
        [Fact]
        public async void GetBorrowerConversationsTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = ChatsHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act
            var claims = new List<Claim>()
            {
                new Claim("UserId", "3")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var actualResult = await handler.GetBorrowerConversations(claimsPrincipal);
            var borrowingIds = dbMock.Borrowings.Where(x => x.UserId == 3).Select(x => x.Id).ToList();
            var expectedResult = mapper.Map<List<Conversation>, List<ConversationGetDto>>(dbMock.Conversations.Where(x => x.BorrowingId is not null && borrowingIds.Contains(x.BorrowingId ?? -1)).ToList());
            // Assert
            Assert.Equal(JsonSerializer.Serialize(actualResult), JsonSerializer.Serialize(expectedResult));
        }
        [Fact]
        public async void GetConversationTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = ChatsHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act
            var actualResult = await handler.GetConversation(3);
            var expectedResult = mapper.Map<Conversation, ConversationGetDto>(dbMock.Conversations.First(x => x.UserConsoleId == 3));
            // Assert
            Assert.Equal(JsonSerializer.Serialize(actualResult), JsonSerializer.Serialize(expectedResult));
        }
        [Fact]
        public async void ContactLenderTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = ChatsHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act
            await handler.ContactLender(10);
            var userConsole = dbMock.UserConsoles.First(x => x.Id == 10);
            var conversation = dbMock.Conversations.First(x => x.UserConsoleId == 10);
            // Assert
            Assert.Equal(JsonSerializer.Serialize(new Conversation { Id = 5, UserConsoleId = 10 }), JsonSerializer.Serialize(conversation));
            Assert.Equal(5, userConsole.ConversationId);
        }
        [Fact]
        public async void ContactBorrowerTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = ChatsHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act
            await handler.ContactBorrower(2);
            var borrowing = dbMock.Borrowings.First(x => x.Id == 2);
            var conversation = dbMock.Conversations.FirstOrDefault(x => x.BorrowingId == 2);
            // Assert
            Assert.Equal(JsonSerializer.Serialize(new Conversation { Id = 5, BorrowingId = 2 }), JsonSerializer.Serialize(conversation));
            Assert.Equal(5, borrowing.ConversationId);
        }
        [Fact]
        public async void SendMessage()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = ChatsHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act
            var claimsAdmin = new List<Claim>()
            {
                new Claim("UserId", "1")
            };
            var identityAdmin = new ClaimsIdentity(claimsAdmin, "TestAuthType");
            var claimsPrincipalAdmin = new ClaimsPrincipal(identityAdmin);
            var claimsLender = new List<Claim>()
            {
                new Claim("UserId", "2")
            };
            var identityLender = new ClaimsIdentity(claimsLender, "TestAuthType");
            var claimsPrincipalLender = new ClaimsPrincipal(identityLender);
            var messages = new List<MessageAddDto> { new MessageAddDto { ConversationId = 2, Text = "Admin message", Files = new List<MessageFileAddDto>() }, new MessageAddDto { ConversationId = 2, Text = "Lender message", Files = new List<MessageFileAddDto>() } };
            await handler.SendMessage(messages[0], claimsPrincipalAdmin);
            await handler.SendMessage(messages[1], claimsPrincipalLender);
            var addedMessages = dbMock.Messages.Where(x => x.ConversationId == 2).TakeLast(2).ToList();
            // Assert
            Assert.Equal("Admin message", addedMessages[0].Text);
            Assert.True(addedMessages[0].FromAdmin);
            Assert.Equal("Lender message", addedMessages[1].Text);
            Assert.False(addedMessages[1].FromAdmin);
        }
    }
}