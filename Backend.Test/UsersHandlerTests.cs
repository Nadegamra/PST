using Xunit;
using Moq.EntityFrameworkCore;
using Moq;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Backend.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using Backend.Handlers;
using Backend.Properties;
using System.Text.Json;
using Backend.Data.Migrations;
using Backend.Test.Mocks;

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
    }
}