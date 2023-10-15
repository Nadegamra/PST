using AutoMapper;
using Backend.Data;
using Backend.Data.Models;
using Backend.Data.Views.Chat;
using Backend.Data.Views.Message;
using Backend.Data.Views.MessageFile;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Backend.Handlers
{
    public class ChatsHandler
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly FilesHandler _filesHandler;

        public ChatsHandler(AppDbContext context, UserManager<User> userManager, IMapper mapper, FilesHandler filesHandler)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _filesHandler = filesHandler;
        }

        public async Task<List<ConversationGetDto>> GetAllConversations()
        {
            List<ConversationGetDto> conversationGets = _mapper.Map<List<Conversation>, List<ConversationGetDto>>(await _context.Conversations.Include(x => x.Messages).ThenInclude(x => x.MessageFiles).Include(x => x.Borrowing).Include(x => x.UserConsole).ThenInclude(x => x.Console).Include(x => x.UserConsole).ThenInclude(x => x.Images).ToListAsync());
            return conversationGets.Where(x => x.Messages.Count > 0).OrderByDescending(x => x.Messages.Max(x => x.DateSent)).ToList().Union(conversationGets).ToList();
        }
        public async Task<List<ConversationGetDto>> GetLenderConversations(ClaimsPrincipal userClaims)
        {
            var user = await _userManager.GetUserAsync(userClaims);
            var userConsoleIds = _context.UserConsoles.Where(x => x.UserId == user.Id).Select(x => x.Id).ToList();
            List<ConversationGetDto> conversationGets = _mapper.Map<List<Conversation>, List<ConversationGetDto>>(await _context.Conversations.Where(x => x.UserConsoleId != null).Where(x => userConsoleIds.Contains(x.UserConsoleId ?? -1)).Include(x => x.Messages).ThenInclude(x => x.MessageFiles).Include(x => x.UserConsole).ThenInclude(x => x.Console).Include(x => x.UserConsole).ThenInclude(x => x.Images).ToListAsync());
            return conversationGets.Where(x => x.Messages.Count > 0).OrderByDescending(x => x.Messages.Max(x => x.DateSent)).ToList().Union(conversationGets).ToList();
        }
        public async Task<List<ConversationGetDto>> GetBorrowerConversations(ClaimsPrincipal userClaims)
        {
            var user = await _userManager.GetUserAsync(userClaims);
            var borrowingIds = _context.Borrowings.Where(x => x.UserId == user.Id).Select(x => x.Id).ToList();
            List<ConversationGetDto> conversationGets = _mapper.Map<List<Conversation>, List<ConversationGetDto>>(await _context.Conversations.Where(x => x.BorrowingId != null).Where(x => borrowingIds.Contains(x.BorrowingId ?? -1)).Include(x => x.Messages).ThenInclude(x => x.MessageFiles).Include(x => x.Borrowing).ThenInclude(x => x.UserConsoles).ThenInclude(x => x.Images).ToListAsync());
            return conversationGets.Where(x => x.Messages.Count > 0).OrderByDescending(x => x.Messages.Max(x => x.DateSent)).ToList().Union(conversationGets).ToList();
        }

        public async Task<ConversationGetDto> GetConversation(int userConsoleId)
        {
            return _mapper.Map<Conversation, ConversationGetDto>(await _context.Conversations.Include(x => x.Messages).ThenInclude(x => x.MessageFiles).Include(x => x.UserConsole).ThenInclude(x => x.Console).Include(x => x.UserConsole).ThenInclude(x => x.Images).Where(x => x.UserConsoleId == userConsoleId).FirstOrDefaultAsync());
        }
        public async Task ContactLender(int userConsoleId)
        {
            if (await _context.Conversations.Where(x => x.UserConsoleId == userConsoleId).FirstOrDefaultAsync() != null)
            {
                return;
            }
            var result = _context.Conversations.Add(new Conversation { UserConsoleId = userConsoleId });
            await _context.SaveChangesAsync();

            var userConsole = _context.UserConsoles.Where(x => x.Id == userConsoleId).First();
            userConsole.ConversationId = result.Entity.Id;
            _context.UserConsoles.Update(userConsole);

            await _context.SaveChangesAsync();
            return;
        }
        public async Task ContactBorrower(int borrowingId)
        {
            if (await _context.Conversations.Where(x => x.BorrowingId == borrowingId).FirstOrDefaultAsync() != null)
            {
                return;
            }
            var result = _context.Conversations.Add(new Conversation { BorrowingId = borrowingId });
            await _context.SaveChangesAsync();

            var borrowing = await _context.Borrowings.Where(x => x.Id == borrowingId).FirstAsync();
            borrowing.ConversationId = result.Entity.Id;
            _context.Borrowings.Update(borrowing);

            await _context.SaveChangesAsync();
            return;
        }
        public async Task SendMessage(MessageAddDto addDto, ClaimsPrincipal userClaims)
        {
            var user = await _userManager.GetUserAsync(userClaims);
            var roles = await _userManager.GetRolesAsync(user);
            List<MessageFileAddDto> files = addDto.Files.ToList();

            addDto.Files = null;
            var result = _context.Messages.Add(new Message { ConversationId = addDto.ConversationId, Text = addDto.Text, FromAdmin = roles[0].ToLower() == "admin", DateSent = DateTime.Now });
            await _context.SaveChangesAsync();

            //Add MessageFiles
            for (int i = 0; i < files.Count; i++)
            {
                files[i].MessageId = result.Entity.Id;
                await _filesHandler.AddMessageFileAsync(files[i]);
            }

        }
    }
}
