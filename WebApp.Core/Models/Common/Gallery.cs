using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models.Base;
using WebApp.Core.Models.Sys;

namespace WebApp.Core.Models.Common
{
    public class Gallery : BaseEntity
    {
        public string Name { get; set; }
        public int? AuthorId { get; set; }
        public User? Author { get; set; }

        public List<PhotoGallery> PhotoGalleries { get; set; }
    }
}
