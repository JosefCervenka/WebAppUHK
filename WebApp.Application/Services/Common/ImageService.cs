using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task AddPhotoAsync(PhotoBase64DTO photo, string name)
        {
            var rawData = _base64Decoder.Decode(photo.Image, out string type);

            var image = new Image()
            {
                Type = type,
                Data = rawData
            };

            await _photoRepository.AddAsync(new Photo
            {
                Image = image, 
                Name = name,
            });
        }
        public async Task<Image> GetImageAsync(int id)
        {
            return await _photoRepository.GetImageAsync(id);
        }
    }
}
