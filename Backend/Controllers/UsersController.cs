using AutoMapper;
using Backend.Data.Models;
using Backend.Data.Views.User;
using Backend.Handlers;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UsersHandler _handler;

        public UsersController(IMapper mapper, UsersHandler handler)
        {
            _mapper = mapper;
            _handler = handler;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("admin/getAll")]
        public async Task<ActionResult> GetAllUsers(string? roleName)
        {
            try
            {
                List<UserGet> users = _mapper.Map<List<User>, List<UserGet>>(await _handler.GetUsers(roleName));
                return Ok(users);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("confirmEmail")]
        public async Task<ActionResult> ConfirmEmail([FromBody] UserEmailConfirmation token)
        {
            try
            {
                token.Token = token.Token.Replace('_', '/');
                await _handler.ConfirmEmail(token.Token);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("passwordReset/send")]
        public async Task<ActionResult> SendPasswordResetEmail(UserForgotPassword data)
        {
            try
            {
                var message = await _handler.GeneratePasswordResetEmail(data.Email);
                MailService.SendEmail(message);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("passwordReset/change")]
        public async Task<ActionResult> ResetPassword([FromBody] UserPasswordReset info)
        {
            try
            {
                info.ResetCode = info.ResetCode.Replace('_', '/');
                await _handler.ResetPassword(info.ResetCode, info.NewPassword);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("account/physical/update")]
        public async Task<ActionResult> UpdatePhysical([FromBody] UserPhysicalUpdate data)
        {
            try
            {
                await _handler.UpdatePhysical(User, data);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("account/legal/update")]
        public async Task<ActionResult> UpdateLegal([FromBody] UserLegalUpdate data)
        {
            try
            {
                await _handler.UpdateLegal(User, data);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("account/address/update")]
        public async Task<ActionResult> UpdateAddress([FromBody] UserAddressUpdate data)
        {
            try
            {
                await _handler.UpdateAddress(User, data);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("account/changePassword")]
        public async Task<ActionResult> ChangePassword([FromBody] UserPasswordChange passwordInfo)
        {
            try
            {
                await _handler.ChangePassword(User, passwordInfo);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("changeEmail/send")]
        public async Task<ActionResult> SendEmailChangeToken([FromBody] UserEmailChange data)
        {
            try
            {
                var message = await _handler.GenerateEmailAddressChangeEmail(User, data.NewEmail);
                MailService.SendEmail(message);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("changeEmail/getUnconfirmed")]
        public async Task<ActionResult> GetUnconfirmedEmails()
        {
            try
            {
                var emails = await _handler.GetUnconfirmedEmails(User);
                return Ok(emails);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost("changeEmail/change")]
        public async Task<ActionResult> ChangeEmail([FromBody] UserEmailConfirmation token)
        {
            try
            {
                token.Token = token.Token.Replace('_', '/');
                await _handler.ChangeEmail(token.Token);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
