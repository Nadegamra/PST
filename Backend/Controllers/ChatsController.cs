using Backend.Data.Models;
using Backend.Data.Views.Chat;
using Backend.Data.Views.Message;
using Backend.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatsController : ControllerBase
    {
        private readonly ChatsHandler _handler;

        public ChatsController(ChatsHandler handler)
        {
            _handler = handler;
        }
        [Authorize(Roles = "admin")]
        [HttpGet("getAll/admin")]

        public async Task<ActionResult<List<ConversationGetDto>>> GetAllConversations()
        {
            try
            {
                return await _handler.GetAllConversations();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "lender")]
        [HttpGet("getAll/lender")]
        public async Task<ActionResult<List<ConversationGetDto>>> GetLenderConversations()
        {
            try
            {
                return await _handler.GetLenderConversations(User);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "borrower")]
        [HttpGet("getAll/borrower")]
        public async Task<ActionResult<List<ConversationGetDto>>> GetBorrowerConversations()
        {
            try
            {
                return await _handler.GetBorrowerConversations(User);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("get/{userConsoleId}")]
        public async Task<ActionResult<ConversationGetDto>> GetConversation(int userConsoleId)
        {
            try
            {
                return await _handler.GetConversation(userConsoleId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpPost("contact/lender/{userConsoleId}")]
        public async Task<ActionResult> ContactLender(int userConsoleId)
        {
            try
            {
                await _handler.ContactLender(userConsoleId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpPost("contact/borrower/{borrowingId}")]
        public async Task<ActionResult> ContactBorrower(int borrowingId)
        {
            try
            {
                await _handler.ContactBorrower(borrowingId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpPost("message")]
        public async Task<ActionResult> SendMessage(MessageAddDto addDto)
        {
            try
            {
                await _handler.SendMessage(addDto, User);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
    }
}
