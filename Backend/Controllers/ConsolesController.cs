using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Backend.Handlers;
using Backend.Data.Views.Console;
using Backend.Data.Views.Image;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsolesController : ControllerBase
    {
        private readonly ConsolesHandler _consolesHandler;
        private readonly FilesHandler _imagesHandler;

        public ConsolesController(ConsolesHandler consolesHandler, FilesHandler imagesHandler)
        {
            _consolesHandler = consolesHandler;
            _imagesHandler = imagesHandler;
        }

        [HttpGet("getAll")]
        public async Task<ActionResult> GetConsoles()
        {
            try
            {
                var result = await _consolesHandler.GetConsolesAsync();

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("get/{id}")]
        public async Task<ActionResult<ImageUploadResult>> GetConsole(int id)
        {
            try
            {
                var result = await _consolesHandler.GetConsoleAsync(id);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpPost("add")]
        public async Task<ActionResult> AddConsole(ConsoleAddDto consoleDto)
        {
            try
            {

                var result = await _consolesHandler.AddConsoleAsync(consoleDto);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpPut("update")]
        public async Task<ActionResult> UpdateConsole(ConsoleUpdateDto consoleDto)
        {
            try
            {
                var result = await _consolesHandler.UpdateConsoleAsync(consoleDto);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpGet("canDelete/{id}")]
        public async Task<ActionResult<bool>> CanDelete(int id)
        {
            try
            {
                return Ok(_consolesHandler.CanDelete(id));

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("remove/{id}")]
        public async Task<ActionResult<ImageUploadResult>> RemoveConsole(int id)
        {
            try
            {
                await _consolesHandler.RemoveConsoleAsync(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        [HttpDelete("images/delete/{id}")]
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
    }
}
