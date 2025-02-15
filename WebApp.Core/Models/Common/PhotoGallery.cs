using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models.Base;

namespace WebApp.Core.Models.Common
{
    public class PhotoGallery : BaseEntity
    {
        public int PhotoId { get; set; }
        public Photo Photo { get; set; }
        public int GalleryId { get; set; }
        public Gallery Gallery { get; set; }
    }
}
