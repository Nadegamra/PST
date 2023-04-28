﻿using Backend.Data.Models;
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

        public async Task SendConfirmationEmail(User user)
        {
            EmailConfirmationToken? token = await _context.EmailConfirmationTokens.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
            if (token != null)
            {
                _context.EmailConfirmationTokens.Remove(token);
            }
            var item = await _context.EmailConfirmationTokens.AddAsync(new EmailConfirmationToken { UserId = user.Id, Token = await _userManager.GenerateEmailConfirmationTokenAsync(user) });
            await _context.SaveChangesAsync();
            token = await _context.EmailConfirmationTokens.Where(x => x.UserId == user.Id).FirstAsync();

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_config.Value.Username),
                Subject = "Account confirmation",
                Body = $"<div>If you have not created this account, you can ignore this email.<br/>Your email confirmation link:<br/>http://localhost:3000/confirmEmail/{item.Entity.Token}</div>",
                IsBodyHtml = true,
            };
            mailMessage.To.Add("karolis.zukaus@gmail.com");

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(_config.Value.Username, _config.Value.Password),
                EnableSsl = true,
            };
            smtpClient.Send(mailMessage);
            return;
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

        public async Task SendPasswordResetEmail(string email)
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
            await _context.PasswordResetTokens.AddAsync(new PasswordResetToken { UserId = user.Id, Token = await _userManager.GeneratePasswordResetTokenAsync(user) });
            await _context.SaveChangesAsync();
            token = await _context.PasswordResetTokens.Where(x => x.UserId == user.Id).FirstAsync();

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_config.Value.Username),
                Subject = "Password reset",
                Body = $"<div>If you have not requested a password reset, you can ignore this email.<br/>Your password reset link:<br/>http://localhost:3000/resetPassword/{token.Token}</div>",
                IsBodyHtml = true,
            };
            mailMessage.To.Add("karolis.zukaus@gmail.com");
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(_config.Value.Username, _config.Value.Password),
                EnableSsl = true,
            };
            smtpClient.Send(mailMessage);
            return;
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

        public async Task<User> UpdatePersonalInfo(ClaimsPrincipal userClaims, User user)
        {
            User original = await _userManager.GetUserAsync(userClaims);
            foreach (var info in user.GetType().GetProperties())
            {
                if (info.GetValue(user) == null || info.Name == "Id")
                {
                    continue;
                }
                info.SetValue(original, info.GetValue(user));
            }
            var result = _context.Users.Update(original);
            await _context.SaveChangesAsync();

            return result.Entity;
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

        public async Task SendEmailAddressChangeEmail(ClaimsPrincipal userClaims, string newEmail)
        {
            User user = await _userManager.GetUserAsync(userClaims);
            EmailChangeToken? token = await _context.EmailChangeTokens.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
            if (token != null)
            {
                _context.EmailChangeTokens.Remove(token);
            }
            await _context.EmailChangeTokens.AddAsync(new EmailChangeToken { UserId = user.Id, Token = await _userManager.GenerateChangeEmailTokenAsync(user, newEmail), NewEmail = newEmail });
            await _context.SaveChangesAsync();
            token = await _context.EmailChangeTokens.Where(x => x.UserId == user.Id).FirstAsync();

            var mailMessage = new MailMessage
            {
                From = new MailAddress("ispagrindai945@gmail.com"),
                Subject = "Email Change",
                Body = $"<div>You have requested to change your email to {newEmail}. If you have not initiated this action, you can ignore this email.<br/>Your email change link:<br/>http://localhost:3000/account/changeEmail/{token.Token}</div>",
                IsBodyHtml = true,
            };
            mailMessage.To.Add("karolis.zukaus@gmail.com");
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("ispagrindai945@gmail.com", "rolczdktsktctrfq"),
                EnableSsl = true,
            };
            smtpClient.Send(mailMessage);
            return;
        }

        public async Task ChangeEmail(ClaimsPrincipal userClaims, string token)
        {
            User user = await _userManager.GetUserAsync(userClaims);
            EmailChangeToken? tokenObj = await _context.EmailChangeTokens.Where(x => x.UserId == user.Id).FirstOrDefaultAsync();
            if (tokenObj == null)
            {
                throw new Exception("You have not requested an emaail change");
            }
            if (tokenObj.Token != token)
            {
                throw new Exception("Invalid action");
            }
            var result = await _userManager.ChangeEmailAsync(user, tokenObj.NewEmail, token);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            user.EmailConfirmed = false;
            await _userManager.UpdateAsync(user);

            await SendConfirmationEmail(userClaims);
        }
    }
}
