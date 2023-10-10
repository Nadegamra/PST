using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Json;

namespace Backend.Test
{
    public class UsersHandlerTests
    {
        [Fact]
        public async void GetUsersTest()
        {
            // Arrange
            var dbMock = DbContextMock.MockDbContext();
            var userManagerMock = UserManagerMock.MockUserManager(MockData.Users.ToList());
            var configMock = new Mock<IOptions<SmtpConfig>>();
            var handler = new UsersHandler(userManagerMock.Object, dbMock.Object, configMock.Object);
            // Act
            var result = await handler.GetUsers(null);
            var result2 = await handler.GetUsers("admin");
            // Assert
            Assert.True(JsonSerializer.Serialize(result) == JsonSerializer.Serialize(MockData.Users));
            Assert.True(JsonSerializer.Serialize(result2) == JsonSerializer.Serialize(new List<User> { MockData.Users[0] }));
        }

        [Fact]
        public async void SendConfirmationEmailTest()
        {
            // Arrange
            var dbMock = DbContextMock.MockDbContext();
            var userManagerMock = UserManagerMock.MockUserManager(MockData.Users.ToList());
            var configMock = new Mock<IOptions<SmtpConfig>>();
            var handler = new UsersHandler(userManagerMock.Object, dbMock.Object, configMock.Object);
            // Act

            // Assert
            Assert.Equal(1, 0);
        }

        [Fact]
        public async void ConfirmEmailTest()
        {
            // Arrange
            var dbMock = DbContextMock.MockDbContext();
            var userManagerMock = UserManagerMock.MockUserManager(MockData.Users.ToList());
            var configMock = new Mock<IOptions<SmtpConfig>>();
            var handler = new UsersHandler(userManagerMock.Object, dbMock.Object, configMock.Object);
            // Act
            await handler.ConfirmEmail("confirmationToken2");
            // Assert
            Assert.Equal(true, dbMock.Object.Users.Where(x => x.Id == 5).First().EmailConfirmed);
        }

        [Fact]
        public async void SendPasswordResetEmailTest()
        {
            // Arrange
            var dbMock = DbContextMock.MockDbContext();
            var userManagerMock = UserManagerMock.MockUserManager(MockData.Users.ToList());
            var configMock = new Mock<IOptions<SmtpConfig>>();
            var handler = new UsersHandler(userManagerMock.Object, dbMock.Object, configMock.Object);
            // Act

            // Assert
            Assert.Equal(1, 0);
        }

        [Fact]
        public async void ResetPasswordTest()
        {
            // Arrange
            var dbMock = DbContextMock.MockDbContext();
            var userManagerMock = UserManagerMock.MockUserManager(MockData.Users.ToList());
            var configMock = new Mock<IOptions<SmtpConfig>>();
            var handler = new UsersHandler(userManagerMock.Object, dbMock.Object, configMock.Object);
            // Act
            await handler.ResetPassword("passwordResetToken1", "NewPassword");
            // Assert
            var token = MockData.PasswordResetTokens.Where(x => x.Token == "passwordResetToken1").First();
            var user = MockData.Users.Where(x => x.Id == token.UserId).First();

            Assert.Equal("NewPassword", user.PasswordHash);
        }

        [Fact]
        public async void UpdatePhysicalTest()
        {
            // Arrange
            var dbMock = DbContextMock.MockDbContext();
            var userManagerMock = UserManagerMock.MockUserManager(MockData.Users.ToList());
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
            var updatedUser = dbMock.Object.Users.Where(x => x.Id == 2).First();
            Assert.Equal("First", updatedUser.FirstName);
            Assert.Equal("Last", updatedUser.LastName);
        }

        [Fact]
        public async void UpdateLegalTest()
        {
            // Arrange
            var dbMock = DbContextMock.MockDbContext();
            var userManagerMock = UserManagerMock.MockUserManager(MockData.Users.ToList());
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
            var updatedUser = dbMock.Object.Users.Where(x => x.Id == 3).First();
            Assert.Equal("420420", updatedUser.CompanyCode);
            Assert.Equal("New Name", updatedUser.CompanyName);
        }

        [Fact]
        public async void UpdateAddressTest()
        {
            // Arrange
            var dbMock = DbContextMock.MockDbContext();
            var userManagerMock = UserManagerMock.MockUserManager(MockData.Users.ToList());
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
            var updatedUser = dbMock.Object.Users.Where(x => x.Id == 2).First();
            Assert.Equal("Country1", updatedUser.Country);
            Assert.Equal("County1", updatedUser.County);
            Assert.Equal("City1", updatedUser.County);
            Assert.Equal("Address1", updatedUser.StreetAddress);
            Assert.Equal("Code", updatedUser.PostalCode);
        }
        [Fact]
        public async void ChangePasswordTest1()
        {
            // Arrange
            var dbMock = DbContextMock.MockDbContext();
            var userManagerMock = UserManagerMock.MockUserManager(MockData.Users.ToList());
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
            var updatedUser = dbMock.Object.Users.Where(x => x.Id == 2).First();
            Assert.Equal("NewPassword", updatedUser.PasswordHash);
        }
        [Fact]
        public async void SendEmailAddressChangeEmailTest()
        {
            // Arrange
            var dbMock = DbContextMock.MockDbContext();
            var userManagerMock = UserManagerMock.MockUserManager(MockData.Users.ToList());
            var configMock = new Mock<IOptions<SmtpConfig>>();
            var handler = new UsersHandler(userManagerMock.Object, dbMock.Object, configMock.Object);
            // Act

            // Assert
            Assert.Equal(1, 0);
        }
        [Fact]
        public async void GetUnconfirmedEmailsTest()
        {
            // Arrange
            var dbMock = DbContextMock.MockDbContext();
            var userManagerMock = UserManagerMock.MockUserManager(MockData.Users.ToList());
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
            string expectedString = JsonSerializer.Serialize(MockData.EmailChangeTokens.Where(x => x.UserId == 2).ToList());
            string actualString = JsonSerializer.Serialize(unconfirmed);
            Assert.Equal(expectedString, actualString);
        }
        [Fact]
        public async void ChangeEmailTest()
        {
            // Arrange
            var dbMock = DbContextMock.MockDbContext();
            var userManagerMock = UserManagerMock.MockUserManager(MockData.Users.ToList());
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