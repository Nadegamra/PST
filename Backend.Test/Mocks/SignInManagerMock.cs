using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Backend.Test.Mocks
{
    public class SignInManagerMock
    {
        public static SignInManager<User> GetMock(Mock<UserManager<User>> userManagerMock)
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
            return signInManagerMock.Object;
        }
    }
}