using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models.Common;
using WebApp.Infrastructure.Repositories.Base;

namespace WebApp.Infrastructure.Repositories.Common
{
    public class PhotoRepository : Repository<Photo>
    {
        public PhotoRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<byte[]> GetImageAsync(int id)
        {
            var photo = await _context.Photo.FirstOrDefaultAsync(x => x.Id == id);
            return photo!.Image;
        }
    }
}
