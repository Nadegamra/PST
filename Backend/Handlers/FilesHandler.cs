using AutoMapper;
using Backend.Data;
using Backend.Data.Models;
using Backend.Properties;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Backend.Data.Views.Image;
using Backend.Data.Views.MessageFile;

namespace Backend.Handlers
{
    public class FilesHandler
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinaryConfig> _config;
        private readonly Cloudinary _cloudinary;
        private readonly bool _disableCloudinary;

        public FilesHandler(AppDbContext context, IMapper mapper, IOptions<CloudinaryConfig> config, bool disableCloudinary = false)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
            _cloudinary = new Cloudinary(new Account(_config.Value.Cloud, _config.Value.ApiKey, _config.Value.ApiSecret));
            _disableCloudinary = disableCloudinary;
        }

        public async Task<List<ImageGetDto>> GetConsoleImagesAsync(int consoleId)
        {
            return _mapper.Map<List<Image>, List<ImageGetDto>>(_context.Consoles.Include(x => x.Images).Where(x => x.Id == consoleId).First().Images.ToList());
        }
        public async Task<List<ImageGetDto>> GetUserConsoleImagesAsync(int consoleId)
        {
            return _mapper.Map<List<Image>, List<ImageGetDto>>(_context.UserConsoles.Include(x => x.Images).Where(x => x.Id == consoleId).First().Images.ToList());
        }
        public async Task<ImageGetDto> GetImageAsync(int id)
        {
            return _mapper.Map<Image, ImageGetDto>(_context.Images.Where(x => x.Id == id).First());
        }
        public async Task AddImageAsync(ImageAddDto imageDto)
        {

            byte[] bytes = Convert.FromBase64String(imageDto.Stream);

            var stream = new MemoryStream(bytes);

            //Cloudinary
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription("file.jpg", stream)
            };
            ImageUploadResult uploadResult;
            if (!_disableCloudinary)
            {
                uploadResult = _cloudinary.Upload(uploadParams);
            }
            else
            {
                uploadResult = new ImageUploadResult() { PublicId = "ImageId" };
            }



            // Database
            Image image;

            if (imageDto.ConsoleId != null)
            {
                image = new Image { Name = imageDto.Name, Path = uploadResult.PublicId, Description = imageDto.Description, ConsoleId = imageDto.ConsoleId };
            }
            else
            {
                image = new Image { Name = imageDto.Name, Path = uploadResult.PublicId, Description = imageDto.Description, UserConsoleId = imageDto.UserConsoleId };

            }

            image.Path = uploadResult.PublicId;

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return;
        }
        public async Task<ImageGetDto> UpdateImageAsync(ImageUpdateDto imageDto)
        {
            // Database
            Image image = _context.Images.Where(x => x.Id == imageDto.Id).First();
            image.Description = imageDto.Description;
            image.Name = imageDto.Name;
            _context.Images.Update(image);
            await _context.SaveChangesAsync();

            return _mapper.Map<Image, ImageGetDto>(image);
        }
        public async Task<ImageGetDto> RemoveImageAsync(int id)
        {
            Image image = _context.Images.Where(x => x.Id == id).First();
            // Cloudinary
            //var cloudinary = new Cloudinary(new Account(_config.Value.Cloud, _config.Value.ApiKey, _config.Value.ApiSecret));
            //await cloudinary.DestroyAsync(new DeletionParams(image.Path));
            // Database
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            return _mapper.Map<Image, ImageGetDto>(image);
        }

        public async Task AddMessageFileAsync(MessageFileAddDto fileDto)
        {

            byte[] bytes = Convert.FromBase64String(fileDto.Stream);

            var stream = new MemoryStream(bytes);

            //Cloudinary
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription("file.jpg", stream)
            };
            ImageUploadResult uploadResult;
            if (!_disableCloudinary)
            {
                uploadResult = _cloudinary.Upload(uploadParams);
            }
            else
            {
                uploadResult = new ImageUploadResult() { PublicId = "ImageId" };
            }


            // Database
            MessageFile file = new MessageFile { Name = fileDto.Name, Path = uploadResult.PublicId, Description = fileDto.Description, MessageId = fileDto.MessageId };

            await _context.MessageFiles.AddAsync(file);
            await _context.SaveChangesAsync();

            return;
        }
    }
}
