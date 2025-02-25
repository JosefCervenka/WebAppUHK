using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApp.Application.Services.Common.Models;
using WebApp.Application.Utils;
using WebApp.Core.Models.Common;
using WebApp.Infrastructure;
using WebApp.Infrastructure.Repositories.Common;

namespace WebApp.Application.Services.Common
{
    public class ImageService
    {
        private readonly AppDbContext _context;

        private readonly PhotoRepository _photoRepository;

        private readonly Base64Decoder _base64Decoder;

        public ImageService(AppDbContext context)
        {
            _context = context;
            _photoRepository = new PhotoRepository(_context);
            _base64Decoder = new Base64Decoder();
        }

        public async Task<(Image? image, string? message)> CreateImageAsync(IFormFile photo)
        {
            if (photo == null || photo.Length == 0)
            {
                return (null, "Not allow file.");
            }
            
            var allowedTypes = new HashSet<string>
            {
                "image/jpeg", "image/png", "image/gif", "image/bmp", "image/webp"
            };
            
            var extension = Path.GetExtension(photo.FileName)?.ToLower();
            
            var allowedExtensions = new HashSet<string> { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };

            if (!allowedTypes.Contains(photo.ContentType) || !allowedExtensions.Contains(extension))
            {
                return (null, "Not allow file.");
            }

            using (var memoryStream = new MemoryStream())
            {
                await photo.CopyToAsync(memoryStream);

                Image image = new Image()
                {
                    Data = memoryStream.ToArray(),
                    Type = photo.ContentType
                };

                return (image, null);
            }
        }
        
        
        public async Task<Image?> GetImageAsync(int id)
        {
            return await _photoRepository.GetImageAsync(id);
        }
    }
}
