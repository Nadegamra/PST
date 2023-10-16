using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Data.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Xunit.Sdk;

namespace Backend.Test.Mocks
{
    public class SignInManagerMock
    {
        public static SignInManager<User> GetMock(Mock<UserManager<User>> userManagerMock, List<User> Users)
        {
            var signInManagerMock = new Mock<SignInManager<User>>(
                userManagerMock.Object,
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<IUserClaimsPrincipalFactory<User>>(),
                null,
                null,
                null,
                null
            );
            signInManagerMock.Setup(x => x.PasswordSignInAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync((User user, string password, bool isPersistent, bool lockoutOnFailure) =>
            {
                var authedUser = Users.FirstOrDefault(x => x.UserName == user.UserName && x.PasswordHash == password);
                return authedUser is null ? SignInResult.Failed : SignInResult.Success;
            });
            return signInManagerMock.Object;
        }
    }
}