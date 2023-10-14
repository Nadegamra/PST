using System.Security.Claims;
using Backend.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Test.Mocks
{
    public class UserManagerMock
    {
        public static Mock<UserManager<User>> MockUserManager(List<User> ls)
        {
            var store = new Mock<IUserStore<User>>();
            var mgr = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<User>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<User>());
            mgr.Setup(x => x.DeleteAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<User, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.Users).Returns(ls.AsQueryable());
            mgr.Setup(x => x.GetUsersInRoleAsync(It.IsAny<string>())).ReturnsAsync((string roleName) =>
            {

                var role = MockData.Roles.Where(x => x.Name == roleName).FirstOrDefault();
                if (role is null)
                {
                    return new List<User>();
                }
                var userIds = MockData.UserRoles.Where(x => x.RoleId == role.Id).Select(x => x.UserId);

                return (IList<User>)MockData.Users.Where(x => userIds.Contains(x.Id)).ToList();
            });
            mgr.Setup(x => x.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync((User user, string token) =>
            {
                EmailConfirmationToken? confirmationToken = MockData.EmailConfirmationTokens.Where(x => x.Token == token).FirstOrDefault();
                if (confirmationToken == null)
                {
                    return IdentityResult.Failed();
                }
                if (confirmationToken.UserId == user.Id)
                {
                    int index = ls.FindIndex(x => x.Id == user.Id);
                    ls[index].EmailConfirmed = true;
                    return IdentityResult.Success;
                }
                else
                {
                    return IdentityResult.Failed();
                }
            });
            mgr.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((string email) =>
            {
                return ls.Where(x => x.Email.ToUpper() == email.ToUpper()).FirstOrDefault();
            });
            mgr.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((string id) =>
            {
                return ls.Where(x => x.Id.ToString() == id).FirstOrDefault();
            });
            mgr.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync((ClaimsPrincipal userClaims) =>
            {
                string id = userClaims.Claims.Where(x => x.Type == "UserId").First().Value;
                return ls.Where(x => x.Id == int.Parse(id)).FirstOrDefault();
            });
            mgr.Setup(x => x.ChangePasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((User user, string currentPassword, string newPassword) =>
            {
                string currentHash = currentPassword;
                string newHash = newPassword;
                int userIndex = ls.FindIndex(x => x.Id == user.Id);
                if (currentHash == user.PasswordHash)
                {
                    ls[userIndex].PasswordHash = newHash;
                    return IdentityResult.Success;
                }
                else
                {
                    return IdentityResult.Failed();
                }
            });
            mgr.Setup(x => x.ChangeEmailAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((User user, string newEmail, string token) =>
            {
                EmailChangeToken? changeToken = MockData.EmailChangeTokens.Where(x => x.Token == token).FirstOrDefault();
                if (changeToken == null)
                {
                    return IdentityResult.Failed();
                }
                if (changeToken.UserId == user.Id)
                {
                    int index = ls.FindIndex(x => x.Id == user.Id);
                    ls[index].Email = newEmail;
                    return IdentityResult.Success;
                }
                else
                {
                    return IdentityResult.Failed();
                }
            });
            mgr.Setup(x => x.ResetPasswordAsync(It.IsAny<User>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((User user, string token, string newPassword) =>
            {
                PasswordResetToken? resetToken = MockData.PasswordResetTokens.Where(x => x.Token == token).FirstOrDefault();
                if (resetToken == null)
                {
                    return IdentityResult.Failed();
                }
                if (resetToken.UserId == user.Id)
                {
                    int index = ls.FindIndex(x => x.Id == user.Id);
                    ls[index].PasswordHash = newPassword;
                    return IdentityResult.Success;
                }
                else
                {
                    return IdentityResult.Failed();
                }
            });
            mgr.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync((User user) =>
            {
                int index = ls.FindIndex(x => x.Id == user.Id);
                ls[index] = user;
                return IdentityResult.Success;
            });
            mgr.Setup(x => x.GeneratePasswordResetTokenAsync(It.IsAny<User>())).ReturnsAsync((User user) =>
            {
                return "NewResetToken";
            });

            return mgr;
        }
    }
}