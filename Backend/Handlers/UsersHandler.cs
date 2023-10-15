using Backend.Data.Models;
using Backend.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using Backend.Properties;
using Microsoft.Extensions.Options;
using Backend.Data.Views.User;

namespace Backend.Handlers
{
    public class UsersHandler
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        private readonly IOptions<SmtpConfig> _config;

        public UsersHandler(UserManager<User> userManager, AppDbContext context, IOptions<SmtpConfig> config)
        {
            _userManager = userManager;
            _context = context;
            _config = config;
        }

        public async Task<List<User>> GetUsers(string? roleName)
        {
            if (roleName == null)
            {
                return _userManager.Users.Select(x => x).ToList();
            }
            return (await _userManager.GetUsersInRoleAsync(roleName)).ToList();
        }

        public async Task<MailMessage> GenerateConfirmationEmail(User user)
        {
            EmailConfirmationToken? token = await _context.EmailConfirmationTokens.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
            if (token != null)
            {
                _context.EmailConfirmationTokens.Remove(token);
            }
            var item = _context.EmailConfirmationTokens.Add(new EmailConfirmationToken { UserId = user.Id, Token = await _userManager.GenerateEmailConfirmationTokenAsync(user) });
            await _context.SaveChangesAsync();
            token = await _context.EmailConfirmationTokens.Where(x => x.UserId == user.Id).FirstAsync();

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_config.Value.Username),
                Subject = "Account confirmation",
                Body = $"<div>If you have not created this account, you can ignore this email.<br/>Your email confirmation link:<br/>http://localhost:3000/confirmEmail/{token.Token.Replace('/', '_')}</div>",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(_config.Value.TestEmail);

            return mailMessage;
        }

        public async Task ConfirmEmail(string confirmationCode)
        {
            EmailConfirmationToken? emailConfirmationToken = await _context.EmailConfirmationTokens.Where(x => x.Token == confirmationCode).FirstOrDefaultAsync();
            if (emailConfirmationToken == null)
            {
                throw new Exception("Email confirmation code is invalid or expired");
            }
            User user = await _context.Users.Where(x => x.Id == emailConfirmationToken.UserId).FirstAsync();
            await _userManager.ConfirmEmailAsync(user, confirmationCode);
            _context.EmailConfirmationTokens.Remove(emailConfirmationToken);
            await _context.SaveChangesAsync();
        }

        public async Task<MailMessage> GeneratePasswordResetEmail(string email)
        {
            User user = await _userManager.FindByEmailAsync(email.ToUpper());
            if (user == null)
            {

                throw new Exception("This email has no associated user");
            }
            PasswordResetToken? token = await _context.PasswordResetTokens.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
            if (token != null)
            {
                _context.PasswordResetTokens.Remove(token);
            }
            _context.PasswordResetTokens.Add(new PasswordResetToken { UserId = user.Id, Token = await _userManager.GeneratePasswordResetTokenAsync(user) });
            await _context.SaveChangesAsync();
            token = await _context.PasswordResetTokens.Where(x => x.UserId == user.Id).FirstAsync();

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_config.Value.Username),
                Subject = "Password reset",
                Body = $"<div>If you have not requested a password reset, you can ignore this email.<br/>Your password reset link:<br/>http://localhost:3000/resetPassword/{token.Token.Replace('/', '_')}</div>",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(_config.Value.TestEmail);
            return mailMessage;
        }

        public async Task ResetPassword(string resetCode, string newPassword)
        {
            PasswordResetToken? token = await _context.PasswordResetTokens.Where(x => x.Token == resetCode).FirstOrDefaultAsync();
            if (token == null)
            {
                throw new Exception("This password reset link is invalid or expired");
            }
            User user = await _userManager.FindByIdAsync(token.UserId.ToString());
            await _userManager.ResetPasswordAsync(user, resetCode, newPassword);
            _context.Remove(token);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePhysical(ClaimsPrincipal userClaims, UserPhysicalUpdate data)
        {
            User original = await _userManager.GetUserAsync(userClaims);
            original.FirstName = data.FirstName;
            original.LastName = data.LastName;
            _context.Users.Update(original);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task UpdateLegal(ClaimsPrincipal userClaims, UserLegalUpdate data)
        {
            User original = await _userManager.GetUserAsync(userClaims);
            original.CompanyCode = data.CompanyCode;
            original.CompanyName = data.CompanyName;
            _context.Users.Update(original);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task UpdateAddress(ClaimsPrincipal userClaims, UserAddressUpdate data)
        {
            User original = await _userManager.GetUserAsync(userClaims);
            original.Country = data.Country;
            original.County = data.County;
            original.City = data.City;
            original.StreetAddress = data.StreetAddress;
            original.PostalCode = data.PostalCode;
            _context.Users.Update(original);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task ChangePassword(ClaimsPrincipal userClaims, UserPasswordChange passwordInfo)
        {
            User user = await _userManager.GetUserAsync(userClaims);
            var result = await _userManager.ChangePasswordAsync(user, passwordInfo.OldPassword, passwordInfo.NewPassword);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }
        }

        public async Task<MailMessage> GenerateEmailAddressChangeEmail(ClaimsPrincipal userClaims, string newEmail)
        {
            User user = await _userManager.GetUserAsync(userClaims);
            EmailChangeToken? token = await _context.EmailChangeTokens.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
            if (token != null)
            {
                _context.EmailChangeTokens.Remove(token);
            }
            _context.EmailChangeTokens.Add(new EmailChangeToken { UserId = user.Id, Token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail), NewEmail = newEmail });
            await _context.SaveChangesAsync();
            token = await _context.EmailChangeTokens.Where(x => x.UserId == user.Id).FirstAsync();

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_config.Value.Username),
                Subject = "Email Change",
                Body = $"<div>You have requested to change your email to {newEmail}. If you have not initiated this action, you can ignore this email.<br/>Your email change link:<br/>http://localhost:3000/changeEmail/{token.Token.Replace('/', '_')}</div>",
                IsBodyHtml = true,
            };
            mailMessage.To.Add(_config.Value.TestEmail);// Replace with newEmail
            return mailMessage;
        }

        public async Task<List<string>> GetUnconfirmedEmails(ClaimsPrincipal userClaims)
        {
            User user = await _userManager.GetUserAsync(userClaims);
            return _context.EmailChangeTokens.Where(x => x.UserId == user.Id).Select(x => x.NewEmail).ToList();
        }

        public async Task ChangeEmail(string token)
        {
            EmailChangeToken? tokenObj = await _context.EmailChangeTokens.Where(x => x.Token == token).FirstOrDefaultAsync();
            if (tokenObj == null)
            {
                throw new Exception("The token is incorrect");
            }
            User user = await _userManager.FindByIdAsync(tokenObj.UserId.ToString());
            var result = await _userManager.ChangeEmailAsync(user, tokenObj.NewEmail, token);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            user.UserName = user.Email;
            user.NormalizedUserName = user.NormalizedEmail;
            await _userManager.UpdateAsync(user);

            _context.EmailChangeTokens.Remove(tokenObj);
            await _context.SaveChangesAsync();
        }
    }
}