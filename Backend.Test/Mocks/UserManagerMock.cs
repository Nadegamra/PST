using Microsoft.AspNetCore.Identity;
using Moq;

namespace Backend.Test.Mocks
{
    public class UserManagerMock
    {
        public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());
            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.Users).Returns(ls.AsQueryable());
            mgr.Setup(x => x.GetUsersInRoleAsync(It.IsAny<string>())).ReturnsAsync((string roleName) =>
            {

                var role = MockData.Roles.Where(x => x.Name == roleName).FirstOrDefault();
                if (role is null)
                {
                    return new List<TUser>();
                }
                var userIds = MockData.UserRoles.Where(x => x.RoleId == role.Id).Select(x => x.UserId);

                return (IList<TUser>)MockData.Users.Where(x => userIds.Contains(x.Id)).ToList();
            });
            return mgr;
        }
    }
}