using System.Security.Claims;
using System.Text.Json;
using Backend.Data.Views.User;
using Backend.Test.Mocks.Handlers;

namespace Backend.Test
{
    public class AuthHandlerTests
    {
        [Fact]
        public async void LoginTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = AuthHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act

            // Assert
            Assert.Equal(1, 0);
        }
        [Fact]
        public async void LogoutTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = AuthHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act

            // Assert
            Assert.Equal(1, 0);
        }
        [Fact]
        public async void GetProfileTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = AuthHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act
            var claims = new List<Claim>()
            {
                new Claim("UserId", "3")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var actualResult = await handler.GetProfile(claimsPrincipal);
            var roleIds = dbMock.UserRoles.Where(x => x.UserId == 3).Select(x => x.RoleId).ToList();
            var roles = dbMock.Roles.Where(x => roleIds.Contains(x.Id)).ToList();
            var expectedResult = mapper.Map<UserGet>(dbMock.Users.First(x => x.Id == 3));
            expectedResult.Role = roles.First().Name;
            // Assert

            Assert.Equal(JsonSerializer.Serialize(expectedResult), JsonSerializer.Serialize(actualResult));

        }
        [Fact]
        public async void RegisterTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = AuthHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act
            await handler.Register(new RegisterPhysical { FirstName = "Firstname", LastName = "Lastnameson", Email = "register_test@example.com", Username = "register_test@example.com", Password = "Password!!!" });
            await handler.Register(new RegisterLegal { CompanyCode = "101010", CompanyName = "Test company", Email = "register_company@example.com", Username = "register_company@example.com", Password = "New!Password" });
            // Assert
            var addedUsers = dbMock.Users.TakeLast(2).ToList();
            Assert.Equal(6, addedUsers[0].Id);
            Assert.Equal("Firstname", addedUsers[0].FirstName);
            Assert.Equal("Lastnameson", addedUsers[0].LastName);
            Assert.Equal("register_test@example.com", addedUsers[0].Email);
            Assert.Equal("register_test@example.com", addedUsers[0].UserName);
            Assert.Equal("Password!!!", addedUsers[0].PasswordHash);

            Assert.Equal(7, addedUsers[1].Id);
            Assert.Equal("101010", addedUsers[1].CompanyCode);
            Assert.Equal("Test company", addedUsers[1].CompanyName);
            Assert.Equal("register_company@example.com", addedUsers[1].Email);
            Assert.Equal("register_company@example.com", addedUsers[1].UserName);
            Assert.Equal("New!Password", addedUsers[1].PasswordHash);
        }
        [Fact]
        public async void GetRegistrationRequestsTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = AuthHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act
            var actualResult = await handler.GetRegistrationRequests();
            var expectedResult = dbMock.RegistrationRequests;
            // Assert
            Assert.Equal(JsonSerializer.Serialize(actualResult), JsonSerializer.Serialize(expectedResult));
        }
        [Fact]
        public async void SubmitRegistrationRequestTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = AuthHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act
            var request = new RegistrationRequest
            {
                CompanyCode = "739271",
                CompanyName = "Yet another company",
                Password = "Password123!"
            };
            await handler.SubmitRegistrationRequest(request);
            var actualResult = dbMock.RegistrationRequests.Last();
            // Assert
            Assert.Equal(JsonSerializer.Serialize(actualResult), JsonSerializer.Serialize(request));
        }
        [Fact]
        public async void ApproveRegistrationRequestTest()
        {
            // Arrange
            var dbMock = new DbContextMock();
            var handler = AuthHandlerMock.GetMock(dbMock);
            var mapper = AutomapperMock.GetMock();
            // Act
            await handler.ApproveRegistrationRequest(new RegistrationRequestApproval { RequestId = 2, IsApproved = true });
            await handler.ApproveRegistrationRequest(new RegistrationRequestApproval { RequestId = 3, IsApproved = false });
            // Assert
            var addedUser = dbMock.Users.Last();
            Assert.Equal(6, addedUser.Id);
            Assert.Equal("Password245!", addedUser.PasswordHash);
            Assert.False(addedUser.EmailConfirmed);
            Assert.Null(dbMock.RegistrationRequests.FirstOrDefault(x => x.Id == 2));
            Assert.Null(dbMock.RegistrationRequests.FirstOrDefault(x => x.Id == 3));
        }
    }
}