using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace Backend.Test.Mocks.Handlers
{
    public class AuthHandlerMock
    {
        public static AuthHandler GetMock(DbContextMock dbMock)
        {
            IMapper mapper = AutomapperMock.GetMock();
            var userManager = UserManagerMock.MockUserManager(dbMock.Users, dbMock.UserRoles, dbMock.Roles);
            SignInManager<User> signInManager = SignInManagerMock.GetMock(userManager);
            UsersHandler usersHandler = UsersHandlerMock.GetMock(dbMock);
            var handler = new AuthHandler(userManager.Object, signInManager, mapper, dbMock.DbMock.Object, usersHandler);
            return handler;
        }
    }
}