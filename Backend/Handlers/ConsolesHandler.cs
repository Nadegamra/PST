using Backend.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Backend.Data.Views.Console;
using Backend.Data.Views.Image;
using System.Reflection;

namespace Backend.Handlers
{
    public class ConsolesHandler
    {
        private readonly AppDbContext _context;
        private readonly FilesHandler _imagesHandler;
        private readonly IMapper _mapper;

        public ConsolesHandler(AppDbContext context, FilesHandler imagesHandler, IMapper mapper)
        {
            _context = context;
            _imagesHandler = imagesHandler;
            _mapper = mapper;
        }

        public async Task<List<ConsoleGetDto>> GetConsolesAsync()
        {
            return _mapper.Map<List<Data.Models.Console>, List<ConsoleGetDto>>(await _context.Consoles.Include(c => c.Images).ToListAsync());
        }
        public async Task<ConsoleGetDto> GetConsoleAsync(int id)
        {
            return _mapper.Map<Data.Models.Console, ConsoleGetDto>(await _context.Consoles.Include(c => c.Images).Where(x => x.Id == id).FirstAsync());
        }
        public async Task<ConsoleGetDto> AddConsoleAsync(ConsoleAddDto consoleDto)
        {
            List<ImageAddDto> images = consoleDto.Images.ToList();

            // Add Console
            Data.Models.Console console = _mapper.Map<ConsoleAddDto, Data.Models.Console>(consoleDto);
            console.Images = null;
            var res = _context.Consoles.Add(console);
            await _context.SaveChangesAsync();

            //Add Images
            for (int i = 0; i < images.Count; i++)
            {
                images[i].ConsoleId = res.Entity.Id;
                await _imagesHandler.AddImageAsync(images[i]);
            }

            Data.Models.Console result = _context.Consoles.Include(x => x.Images).Where(x => x.Id == res.Entity.Id).First();
            return _mapper.Map<Data.Models.Console, ConsoleGetDto>(result);
        }
        public async Task<ConsoleGetDto> UpdateConsoleAsync(ConsoleUpdateDto consoleDto)
        {
            // Update Images
            List<ImageUpdateDto> images = consoleDto.Images.ToList();
            foreach (ImageUpdateDto image in images)
            {
                await _imagesHandler.UpdateImageAsync(image);
            }

            // Update Console
            Data.Models.Console changes = _mapper.Map<ConsoleUpdateDto, Data.Models.Console>(consoleDto);

            Data.Models.Console original = _context.Consoles.First(x => x.Id == changes.Id);
            foreach (PropertyInfo info in original.GetType().GetProperties())
            {
                if (info.GetValue(changes) == null || info.Name == "Id")
                {
                    continue;
                }
                info.SetValue(original, info.GetValue(changes));
            }

            _context.Consoles.Update(original);
            await _context.SaveChangesAsync();

            Data.Models.Console result = _context.Consoles.Include(x => x.Images).Where(x => x.Id == consoleDto.Id).First();
            return _mapper.Map<Data.Models.Console, ConsoleGetDto>(result);
        }
        public async Task RemoveConsoleAsync(int id)
        {
            var userConsoles = _context.UserConsoles.FirstOrDefault(x => x.ConsoleId == id);
            if (userConsoles is not null)
            {
                throw new InvalidOperationException("Can't delete: has associated UserConsoles");
            }

            // Remove Images
            List<int> imagesIds = (await _imagesHandler.GetConsoleImagesAsync(id)).Select(x => x.Id).ToList();
            foreach (int imageId in imagesIds)
            {
                await _imagesHandler.RemoveImageAsync(imageId);
            }

            // Remove Console
            Data.Models.Console console = await _context.Consoles.Where(x => x.Id == id).FirstAsync();
            _context.Consoles.Remove(console);
            await _context.SaveChangesAsync();

            return;
        }
        public bool CanDelete(int id)
        {
            return !_context.UserConsoles.Where(x => x.ConsoleId == id).Any();
        }
    }
}
