using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Application.Services.Common.Models
{
    public class GalleryWithPhotosDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Picture> Photos { get; set; }
    }

    public class Picture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
