using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models.Common;
using WebApp.Core.Models.Sys;
using WebApp.Infrastructure.Repositories.Base;

namespace WebApp.Infrastructure.Repositories.Common
{
    public class GalleryRepository : Repository<Gallery>
    {
        public async Task<Gallery?> GetWithPhotoAsync(int id)
        {
            return await _context.Gallery
                .Include(x => x.PhotoGalleries)
                    .ThenInclude(x => x.Photo)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddPhoto(int galleryId, Photo photo)
        {
            var gallery = await _context.Gallery
                .Include(x => x.PhotoGalleries)
                .FirstOrDefaultAsync(x => x.Id == galleryId);

            gallery.PhotoGalleries.Add(new PhotoGallery
            {
                Photo = photo
            });

            (await _context.Gallery.FirstOrDefaultAsync(x => x.Id == galleryId))?.PhotoGalleries.Add(new PhotoGallery
            {
                Photo = photo
            });

            await _context.SaveChangesAsync();
        }

        public List<Gallery> GetAllUserGalleries(int userId, bool withPhotos = false)
        {
            if (withPhotos == true)
                return _context.Gallery.Where(x => x.AuthorId == userId)
                    .Include(x => x.PhotoGalleries)
                    .ToList();

            else
                return _context.Gallery.Where(x => x.AuthorId == userId).ToList();
        }

        public GalleryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
