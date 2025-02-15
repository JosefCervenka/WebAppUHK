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
    public class GalleryService
    {
        private readonly AppDbContext _context;

        private readonly GalleryRepository _galleryRepository;

        private readonly PhotoRepository _photoRepository;

        private readonly Base64Decoder _base64Decoder;

        public GalleryService(AppDbContext context)
        {
            _context = context;
            _galleryRepository = new GalleryRepository(_context);
            _photoRepository = new PhotoRepository(_context);
            _base64Decoder = new Base64Decoder();
        }
        public async Task<GalleryWithPhotosDTO?> GetGalleryAsync(int id)
        {
            var gallery = await _galleryRepository.GetWithPhotoAsync(id);

            GalleryWithPhotosDTO galleryDTO = new GalleryWithPhotosDTO
            {
                Id = gallery.Id,
                Name = gallery.Name,
                Photos = gallery.Photos.Select(x => new Picture
                {
                    Name = x.Name,
                    Id = x.Id,
                    Url = $"/api/image/{x.ImageId}"
                }).ToList()
            };

            return galleryDTO;
        }

        public async Task AddGalleryAsync(GalleryDTO gallery, int userId)
        {
            await _galleryRepository.AddAsync(new Gallery()
            {
                AuthorId = userId,
                Name = gallery.Name,
            });

            await _context.SaveChangesAsync();
        }

        public async Task<PhotoDTO?> GetPhotoAsync(int id)
        {
            var photo = await _photoRepository.GetAsync(id);

            if (photo is null)
                return null;

            return new PhotoDTO
            {
                Name = photo.Name,
                Image = $"/api/image/{photo.Id}"
            };
        }

        public async Task AddPhotoAsync(PhotoBase64DTO photo, int galleryId)
        {
            var rawData = _base64Decoder.Decode(photo.Image, out string type);

            await _galleryRepository.AddPhoto(galleryId, new Photo()
            {
                Name = photo.Name,

                Image = new Image()
                {
                    Type = type,
                    Data = rawData
                }
            });
        }
        public async Task<Image> GetImageAsync(int id)
        {
            return await _photoRepository.GetImageAsync(id);
        }
    }
}
