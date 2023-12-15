using Backend.Data.Views.Console;
using Backend.Data.Views.Image;
using Backend.Data.Views.UserConsole;
using Backend.Handlers;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserConsolesController : ControllerBase
    {
        private readonly UserConsolesHandler _userConsolesHandler;
        private readonly FilesHandler _imagesHandler;

        public UserConsolesController(UserConsolesHandler handler)
        {
            _userConsolesHandler = handler;
        }

        [HttpGet("get")]
        public async Task<ActionResult> GetUserConsoles()
        {
            try
            {
                var result = await _userConsolesHandler.GetUserConsolesAsync(User);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("get/filter")]
        public async Task<ActionResult> GetUserConsolesByStatus(UserConsolesStatusRequest status)
        {
            try
            {
                var result = await _userConsolesHandler.GetUserConsolesByStatusAsync(User, status.ConsoleStatus);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/{id}")]
        public async Task<ActionResult<ImageUploadResult>> GetUserConsole(int id)
        {
            try
            {
                var result = await _userConsolesHandler.GetUserConsoleAsync(id);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "lender")]
        [HttpPost("add")]
        public async Task<ActionResult> AddUserConsole(UserConsoleAddDto consoleDto)
        {
            try
            {

                var result = await _userConsolesHandler.AddUserConsoleAsync(consoleDto, User);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
        [Authorize(Roles = "lender")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateUserConsole(UserConsoleUpdateDto consoleDto)
        {
            try
            {
                var result = await _userConsolesHandler.UpdateUserConsoleAsync(consoleDto);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("remove/{id}")]
        public async Task<ActionResult<ImageUploadResult>> RemoveUserConsole(int id)
        {
            try
            {
                await _userConsolesHandler.RemoveUserConsoleAsync(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "lender")]
        [HttpPost("images/add")]
        public async Task<ActionResult> AddImage(ImageAddDto imageDto)
        {
            try
            {
                await _imagesHandler.AddImageAsync(imageDto);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "lender")]
        [HttpDelete("images/delete")]
        public async Task<ActionResult> RemoveImage(int id)
        {
            try
            {
                await _imagesHandler.RemoveImageAsync(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpPatch("updateStatus")]
        public async Task<ActionResult> UpdateStatus(UserConsoleStatusUpdateDto updateDto)
        {
            try
            {
                await _userConsolesHandler.UpdateStatus(updateDto, User);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "lender")]
        [HttpPatch("terminateLender/{id}")]
        public async Task<ActionResult> TerminateContractByLender(int id)
        {
            try
            {
                await _userConsolesHandler.UpdateStatus(new UserConsoleStatusUpdateDto { Id = id, ConsoleStatus = Data.Models.UserConsoleStatus.AWAITING_TERMINATION_BY_LENDER }, User);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "borrower")]
        [HttpPatch("terminateBorrower/{id}")]
        public async Task<ActionResult> TerminateContractByBorrower(int id)
        {
            try
            {
                await _userConsolesHandler.UpdateStatus(new UserConsoleStatusUpdateDto { Id = id, ConsoleStatus = Data.Models.UserConsoleStatus.AWAITING_TERMINATION_BY_BORROWER }, User);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
