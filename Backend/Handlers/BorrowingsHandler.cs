using AutoMapper;
using Backend.Data;
using Backend.Data.Models;
using Microsoft.AspNetCore.Identity;
using Backend.Data.Views.BorrowedConsole;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Backend.Data.Views.UserConsole;
using Backend.Data.Migrations;

namespace Backend.Handlers
{
    public class BorrowingsHandler
    {
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly UserConsolesHandler _userConsolesHandler;

        public BorrowingsHandler(IMapper mapper, AppDbContext context, UserManager<User> userManager, UserConsolesHandler userConsolesHandler)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _userConsolesHandler = userConsolesHandler;
        }

        public async Task<List<BorrowingGetDto>> GetAllAsync()
        {
            return _mapper.Map<List<Borrowing>, List<BorrowingGetDto>>(await _context.Borrowings.Include(x => x.UserConsoles).ThenInclude(x => x.Images).Include(x => x.UserConsoles).ThenInclude(x => x.Console).ToListAsync());
        }
        public async Task<List<BorrowingGetDto>> GetByUserAsync(ClaimsPrincipal userClaims)
        {
            User user = await _userManager.GetUserAsync(userClaims);
            return _mapper.Map<List<Borrowing>, List<BorrowingGetDto>>(await _context.Borrowings.Include(x => x.UserConsoles).ThenInclude(x => x.Images).Include(x => x.UserConsoles).ThenInclude(x => x.Console).Where(x=>x.UserId == user.Id).ToListAsync());
        }
        public async Task<BorrowingGetDto> GetByIdAsync(int id)
        {
            return _mapper.Map<Borrowing, BorrowingGetDto>(await _context.Borrowings.Include(x=>x.User).Include(x => x.UserConsoles).ThenInclude(x => x.Images).Include(x => x.UserConsoles).ThenInclude(x => x.Console).Where(x=>x.Id == id).FirstAsync());
        }
        public async Task AddAsync(BorrowingAddDto addDto, ClaimsPrincipal userClaims)
        {
            User user = await _userManager.GetUserAsync(userClaims);
            
            var result = _context.Borrowings.Add(new Borrowing { UserId = user.Id, Status = BorrowingStatus.PENDING });
            await _context.SaveChangesAsync();

            async Task UpdateUserConsole(int userConsoleId, int borrowingId)
            {
                var userConsole = await _context.UserConsoles.Where(x => x.Id == userConsoleId).FirstAsync();

                userConsole.BorrowingId = borrowingId;
                userConsole.ConsoleStatus = UserConsoleStatus.RESERVED;

                _context.UserConsoles.Update(userConsole);
                await _context.SaveChangesAsync();
            }


            foreach(var id in addDto.UserConsoleIds)
            {
                var userConsole = await _context.UserConsoles.Where(x=>x.Id == id).FirstAsync();
                if(userConsole == null || userConsole.ConsoleStatus != UserConsoleStatus.AT_PLATFORM)
                {
                    continue;
                }
                await UpdateUserConsole(id, result.Entity.Id);
            }
        }

        public async Task UpdateAsync(BorrowingUpdateDto updateDto, ClaimsPrincipal userClaims)
        {
            //var borrowing = await _context.Borrowings.Where(x=>x.Id == updateDto.Id).Include(x=>x.UserConsoles).FirstAsync();
            //var originalUserConsoleIds = borrowing.UserConsoles.Select(x=>x.Id).ToList();

            var originalUserConsoleIds = await _context.UserConsoles.Where(x => x.BorrowingId == updateDto.Id).Select(x => x.Id).ToListAsync();

            foreach (var id in originalUserConsoleIds)
            {
                await _userConsolesHandler.UpdateStatus(new UserConsoleStatusUpdateDto { Id = id, ConsoleStatus = UserConsoleStatus.AT_PLATFORM }, userClaims);
            }

            foreach(var id in updateDto.UserConsoleIds)
            {
                await _userConsolesHandler.UpdateStatus(new UserConsoleStatusUpdateDto { Id = id, ConsoleStatus = UserConsoleStatus.AT_LENDER }, userClaims);
            }
        }
        public async Task UpdateStatusAsync(BorrowingUpdateStatusDto statusDto)
        {
            var userConsoles = await _context.UserConsoles.Where(x => x.BorrowingId == statusDto.Id).ToListAsync();

            async Task UpdateUserConsole(int userConsoleId, UserConsoleStatus status)
            {
                var userConsole = await _context.UserConsoles.Where(x => x.Id == userConsoleId).FirstAsync();

                userConsole.ConsoleStatus = status;

                _context.UserConsoles.Update(userConsole);
                await _context.SaveChangesAsync();
            }

            var borrowing = await _context.Borrowings.Where(x => x.Id == statusDto.Id).FirstAsync();
            borrowing.Status = statusDto.BorrowingStatus;
            await _context.SaveChangesAsync();

            if(statusDto.BorrowingStatus == BorrowingStatus.ACTIVE)
            {
                foreach(var userConsole in userConsoles)
                {
                    await UpdateUserConsole(userConsole.Id, UserConsoleStatus.AT_LENDER);
                }
            }
        }
        public async Task DeleteAsync(int id)
        {
            var conversation = await _context.Conversations.Where(x=>x.BorrowingId == id).FirstOrDefaultAsync();
            if(conversation != null)
            {
                _context.Conversations.Remove(conversation);
            }

            var borrowing = await _context.Borrowings.Where(x=>x.Id == id).FirstAsync();

            _context.Borrowings.Remove(borrowing);
            await _context.SaveChangesAsync();
        }

        public bool CanDelete(int id)
        {
            return !_context.UserConsoles.Where(x=>x.BorrowingId == id).Any();
        }
    }
}
