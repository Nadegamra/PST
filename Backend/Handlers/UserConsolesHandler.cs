﻿using AutoMapper;
using Backend.Data;
using Backend.Data.Models;
using Backend.Data.Views.Image;
using Backend.Data.Views.UserConsole;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;

namespace Backend.Handlers
{
    public class UserConsolesHandler
    {
        private readonly IMapper _mapper;
        private readonly FilesHandler _imagesHandler;
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserConsolesHandler(IMapper mapper, FilesHandler imagesHandler, AppDbContext context, UserManager<User> userManager)
        {
            _mapper = mapper;
            _imagesHandler = imagesHandler;
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<UserConsoleGetDto>> GetUserConsolesAsync(ClaimsPrincipal claims)
        {
            User user = await _userManager.GetUserAsync(claims);
            return _mapper.Map<List<UserConsole>, List<UserConsoleGetDto>>(_context.UserConsoles.Include(x => x.Images).Include(x => x.Console).Include(x => x.Conversation).Where(x => x.UserId == user.Id).ToList());
        }
        public async Task<List<UserConsoleGetDto>> GetUserConsolesByStatusAsync(ClaimsPrincipal claims, UserConsoleStatus status)
        {
            User user = await _userManager.GetUserAsync(claims);
            return _mapper.Map<List<UserConsole>, List<UserConsoleGetDto>>(_context.UserConsoles.Include(x => x.Images).Include(x => x.Console).Include(x => x.Conversation).Where(x => x.ConsoleStatus == status).ToList());
        }
        public async Task<UserConsoleGetDto> GetUserConsoleAsync(int id)
        {
            return _mapper.Map<UserConsole, UserConsoleGetDto>(_context.UserConsoles.Include(x => x.Images).Include(x => x.Console).Include(x => x.Conversation).Include(x => x.User).Where(x => x.Id == id).First());
        }
        public async Task<UserConsoleGetDto> AddUserConsoleAsync(UserConsoleAddDto userConsoleDto, ClaimsPrincipal claims)
        {
            var user = await _userManager.GetUserAsync(claims);

            List<ImageAddDto> images = userConsoleDto.Images.ToList();

            // Add Console
            UserConsole userConsole = _mapper.Map<UserConsoleAddDto, UserConsole>(userConsoleDto);
            userConsole.Images = null;
            userConsole.ConsoleStatus = UserConsoleStatus.UNCONFIRMED;
            userConsole.UserId = user.Id;
            var res = _context.UserConsoles.Add(userConsole);
            await _context.SaveChangesAsync();

            //Add Images
            for (int i = 0; i < images.Count; i++)
            {
                images[i].UserConsoleId = res.Entity.Id;
                await _imagesHandler.AddImageAsync(images[i]);
            }

            UserConsole result = _context.UserConsoles.Include(x => x.Images).Where(x => x.Id == res.Entity.Id).First();
            return _mapper.Map<UserConsole, UserConsoleGetDto>(result);
        }

        public async Task<UserConsoleGetDto> UpdateUserConsoleAsync(UserConsoleUpdateDto userConsoleDto)
        {
            // Update Images
            List<ImageUpdateDto> images = userConsoleDto.Images.ToList();
            foreach (ImageUpdateDto image in images)
            {
                await _imagesHandler.UpdateImageAsync(image);
            }

            // Update Console
            UserConsole changes = _mapper.Map<UserConsoleUpdateDto, UserConsole>(userConsoleDto);

            UserConsole original = _context.UserConsoles.Where(x => x.Id == userConsoleDto.Id).First();
            foreach (PropertyInfo info in original.GetType().GetProperties())
            {
                if (info.GetValue(changes) == null || info.Name == "Id")
                {
                    continue;
                }
                info.SetValue(original, info.GetValue(changes));
            }

            _context.UserConsoles.Update(original);
            await _context.SaveChangesAsync();

            UserConsole result = _context.UserConsoles.Include(x => x.Images).Where(x => x.Id == userConsoleDto.Id).First();
            return _mapper.Map<UserConsole, UserConsoleGetDto>(result);
        }

        public async Task RemoveUserConsoleAsync(int id)
        {
            // Remove conversation
            var conversation = await _context.Conversations.Where(x => x.UserConsoleId == id).FirstOrDefaultAsync();
            if (conversation != null)
            {
                _context.Conversations.Remove(conversation);
            }

            // Remove Images
            List<int> imagesIds = (await _imagesHandler.GetUserConsoleImagesAsync(id)).Select(x => x.Id).ToList();
            foreach (int imageId in imagesIds)
            {
                await _imagesHandler.RemoveImageAsync(imageId);
            }

            // Remove Console
            UserConsole userConsole = await _context.UserConsoles.Where(x => x.Id == id).FirstAsync();

            if (userConsole.ConsoleStatus != UserConsoleStatus.UNCONFIRMED)
            {
                throw new InvalidOperationException("");
            }

            _context.UserConsoles.Remove(userConsole);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task UpdateStatus(UserConsoleStatusUpdateDto status, ClaimsPrincipal userClaims)
        {
            var user = await _userManager.GetUserAsync(userClaims);
            var role = (await _userManager.GetRolesAsync(user)).First().ToLower();

            UserConsoleStatus consoleStatus = status.ConsoleStatus;

            if (role == "admin" &&
                (consoleStatus == UserConsoleStatus.RESERVED
                || consoleStatus == UserConsoleStatus.AWAITING_TERMINATION_BY_LENDER
                || consoleStatus == UserConsoleStatus.AWAITING_TERMINATION_BY_BORROWER))
            {
                throw new Exception("Invalid action");
            }

            if (role == "lender" && consoleStatus != UserConsoleStatus.AWAITING_TERMINATION_BY_LENDER)
            {
                throw new Exception("Invalid action");
            }

            if(role == "borrower" && consoleStatus != UserConsoleStatus.AWAITING_TERMINATION_BY_BORROWER)
            {
                throw new Exception("Invalid action");
            }

            UserConsole userConsole = _context.UserConsoles.Where(x=>x.Id == status.Id).First();
            userConsole.ConsoleStatus = status.ConsoleStatus;
            if(status.ConsoleStatus == UserConsoleStatus.UNCONFIRMED || status.ConsoleStatus == UserConsoleStatus.AT_PLATFORM)
            {
                userConsole.BorrowingId = null;
            }
            _context.UserConsoles.Update(userConsole);
            await _context.SaveChangesAsync();
            return;
        }
    }
}
