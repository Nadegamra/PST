using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Security.Claims;
using System.Text.Json;

namespace Backend.Test
{
    public class UsersHandlerTests
    {
        private static SmtpConfig _configData = new SmtpConfig { Username = "email@example.com", TestEmail = "tester@example.com", Password = "Password" };
        [Fact]
        public async void GetUsersTest()
        {
            // Arrange
            var mock = new DbContextMock();
            var dbMock = mock.DbMock;
            var userManagerMock = UserManagerMock.MockUserManager(mock.Users);
            var configMock = new Mock<IOptions<SmtpConfig>>();
            var handler = new UsersHandler(userManagerMock.Object, dbMock.Object, configMock.Object);
            // Act
            var result = await handler.GetUsers(null);
            var result2 = await handler.GetUsers("admin");
            // Assert
            Assert.True(JsonSerializer.Serialize(result) == JsonSerializer.Serialize(mock.Users));
            Assert.True(JsonSerializer.Serialize(result2) == JsonSerializer.Serialize(new List<User> { mock.Users[0] }));
        }

        [Fact]
        public async void GenerateConfirmationEmailTest()
        {
            // Arrange
            var mock = new DbContextMock();
            var dbMock = mock.DbMock;
            var userManagerMock = UserManagerMock.MockUserManager(mock.Users);
            var configMock = new Mock<IOptions<SmtpConfig>>();
            configMock.Setup(x => x.Value).Returns(_configData);
            var handler = new UsersHandler(userManagerMock.Object, dbMock.Object, configMock.Object);
            // Act

            // Assert
            Assert.Equal(1, 0);
        }

        [Fact]
        public async void ConfirmEmailTest()
        {
            // Arrange
            var mock = new DbContextMock();
            var dbMock = mock.DbMock;
            var userManagerMock = UserManagerMock.MockUserManager(mock.Users);
            var configMock = new Mock<IOptions<SmtpConfig>>();
            var handler = new UsersHandler(userManagerMock.Object, dbMock.Object, configMock.Object);
            // Act
            await handler.ConfirmEmail("confirmationToken2");
            // Assert
            Assert.Equal(true, mock.Users.Where(x => x.Id == 5).First().EmailConfirmed);
        }

        [Fact]
        public async void GeneratePasswordResetEmailTest()
        {
            // Arrange
            var mock = new DbContextMock();
            var dbMock = mock.DbMock;
            var userManagerMock = UserManagerMock.MockUserManager(mock.Users);
            var configMock = new Mock<IOptions<SmtpConfig>>();
            configMock.Setup(x => x.Value).Returns(_configData);
            var handler = new UsersHandler(userManagerMock.Object, dbMock.Object, configMock.Object);
            // Act
            var actualResult = await handler.GeneratePasswordResetEmail("company@example.com");
            var token = dbMock.Object.PasswordResetTokens.Where(x => x.Id == 3).First();
            var expectedResult = new MailMessage
            {
                From = new MailAddress(_configData.Username),
                Subject = "Password reset",
                Body = $"<div>If you have not requested a password reset, you can ignore this email.<br/>Your password reset link:<br/>http://localhost:3000/resetPassword/{token.Token.Replace('/', '_')}</div>",
                IsBodyHtml = true,
            };
            expectedResult.To.Add(_configData.TestEmail);

            // Assert
            Assert.Equal(JsonSerializer.Serialize(actualResult.From), JsonSerializer.Serialize(expectedResult.From));
            Assert.Equal(JsonSerializer.Serialize(actualResult.To), JsonSerializer.Serialize(expectedResult.To));
            Assert.Equal(actualResult.Subject, expectedResult.Subject);
            Assert.Equal(actualResult.Body, expectedResult.Body);
            Assert.Equal(actualResult.IsBodyHtml, expectedResult.IsBodyHtml);
        }

        [Fact]
        public async void ResetPasswordTest()
        {
            // Arrange
            var mock = new DbContextMock();
            var dbMock = mock.DbMock;
            var userManagerMock = UserManagerMock.MockUserManager(mock.Users);
            var configMock = new Mock<IOptions<SmtpConfig>>();
            var handler = new UsersHandler(userManagerMock.Object, dbMock.Object, configMock.Object);
            // Act
            await handler.ResetPassword("passwordResetToken1", "NewPassword");
            // Assert
            var token = mock.PasswordResetTokens.Where(x => x.Token == "passwordResetToken1").First();
            var user = mock.Users.Where(x => x.Id == token.UserId).First();

            Assert.Equal("NewPassword", user.PasswordHash);
        }

        [Fact]
        public async void UpdatePhysicalTest()
        {
            // Arrange
            var mock = new DbContextMock();
            var dbMock = mock.DbMock;
            var userManagerMock = UserManagerMock.MockUserManager(mock.Users);
            var configMock = new Mock<IOptions<SmtpConfig>>();
            var handler = new UsersHandler(userManagerMock.Object, dbMock.Object, configMock.Object);

            var claims = new List<Claim>()
            {
                new Claim("UserId", "2")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            // Act
            await handler.UpdatePhysical(claimsPrincipal, new Data.Views.User.UserPhysicalUpdate
            {
                FirstName = "First",
                LastName = "Last"
            });
            // Assert
            var updatedUser = mock.Users.Where(x => x.Id == 2).First();
            Assert.Equal("First", updatedUser.FirstName);
            Assert.Equal("Last", updatedUser.LastName);
        }

        [Fact]
        public async void UpdateLegalTest()
        {
            // Arrange
            var mock = new DbContextMock();
            var dbMock = mock.DbMock;
            var userManagerMock = UserManagerMock.MockUserManager(mock.Users);
            var configMock = new Mock<IOptions<SmtpConfig>>();
            var handler = new UsersHandler(userManagerMock.Object, dbMock.Object, configMock.Object);
            // Act
            var claims = new List<Claim>()
            {
                new Claim("UserId", "3")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            await handler.UpdateLegal(claimsPrincipal, new Data.Views.User.UserLegalUpdate
            {
                CompanyCode = "420420",
                CompanyName = "New Name"
            });
            // Assert
            var updatedUser = mock.Users.Where(x => x.Id == 3).First();
            Assert.Equal("420420", updatedUser.CompanyCode);
            Assert.Equal("New Name", updatedUser.CompanyName);
        }

        [Fact]
        public async void UpdateAddressTest()
        {
            // Arrange
            var mock = new DbContextMock();
            var dbMock = mock.DbMock;
            var userManagerMock = UserManagerMock.MockUserManager(mock.Users);
            var configMock = new Mock<IOptions<SmtpConfig>>();
            var handler = new UsersHandler(userManagerMock.Object, dbMock.Object, configMock.Object);
            // Act
            var claims = new List<Claim>()
            {
                new Claim("UserId", "2")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            await handler.UpdateAddress(claimsPrincipal, new Data.Views.User.UserAddressUpdate
            {
                Country = "Country1",
                County = "County1",
                City = "City1",
                StreetAddress = "Address1",
                PostalCode = "Code"
            });
            // Assert
            var updatedUser = mock.Users.Where(x => x.Id == 2).First();
            Assert.Equal("Country1", updatedUser.Country);
            Assert.Equal("County1", updatedUser.County);
            Assert.Equal("City1", updatedUser.City);
            Assert.Equal("Address1", updatedUser.StreetAddress);
            Assert.Equal("Code", updatedUser.PostalCode);
        }
        [Fact]
        public async void ChangePasswordTest1()
        {
            // Arrange
            var mock = new DbContextMock();
            var dbMock = mock.DbMock;
            var userManagerMock = UserManagerMock.MockUserManager(mock.Users);
            var configMock = new Mock<IOptions<SmtpConfig>>();
            var handler = new UsersHandler(userManagerMock.Object, dbMock.Object, configMock.Object);
            // Act
            var claims = new List<Claim>()
            {
                new Claim("UserId", "2")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            await handler.ChangePassword(claimsPrincipal, new Data.Views.User.UserPasswordChange
            {
                OldPassword = "Password",
                NewPassword = "NewPassword"
            });
            // Assert
            var updatedUser = mock.Users.Where(x => x.Id == 2).First();
            Assert.Equal("NewPassword", updatedUser.PasswordHash);
        }
        [Fact]
        public async void GenerateEmailAddressChangeEmailTest()
        {
            // Arrange
            var mock = new DbContextMock();
            var dbMock = mock.DbMock;
            var userManagerMock = UserManagerMock.MockUserManager(mock.Users);
            var configMock = new Mock<IOptions<SmtpConfig>>();
            configMock.Setup(x => x.Value).Returns(_configData);
            var handler = new UsersHandler(userManagerMock.Object, dbMock.Object, configMock.Object);
            // Act

            // Assert
            Assert.Equal(1, 0);
        }
        [Fact]
        public async void GetUnconfirmedEmailsTest()
        {
            // Arrange
            var mock = new DbContextMock();
            var dbMock = mock.DbMock;
            var userManagerMock = UserManagerMock.MockUserManager(mock.Users);
            var configMock = new Mock<IOptions<SmtpConfig>>();
            var handler = new UsersHandler(userManagerMock.Object, dbMock.Object, configMock.Object);
            // Act
            var claims = new List<Claim>()
            {
                new Claim("UserId", "2")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            List<string> unconfirmed = await handler.GetUnconfirmedEmails(claimsPrincipal);
            // Assert
            string expectedString = JsonSerializer.Serialize(mock.EmailChangeTokens.Where(x => x.UserId == 2).Select(x => x.NewEmail).ToList());
            string actualString = JsonSerializer.Serialize(unconfirmed);
            Assert.Equal(expectedString, actualString);
        }
        [Fact]
        public async void ChangeEmailTest()
        {
            // Arrange
            var mock = new DbContextMock();
            var dbMock = mock.DbMock;
            var userManagerMock = UserManagerMock.MockUserManager(mock.Users);
            var configMock = new Mock<IOptions<SmtpConfig>>();
            var handler = new UsersHandler(userManagerMock.Object, dbMock.Object, configMock.Object);
            // Act
            await handler.ChangeEmail("emailChangeToken2");
            // Assert
            var updatedUser = dbMock.Object.Users.Where(x => x.Id == 3).First();
            Assert.Equal("owner@example.com", updatedUser.Email);
        }
    }
}