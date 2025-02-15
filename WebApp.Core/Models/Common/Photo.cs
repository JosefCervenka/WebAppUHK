using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models.Base;

namespace WebApp.Core.Models.Common
{
    public class Photo : BaseEntity
    {
        public int ImageId { get; set; }
        public Image Image { get; set; }
        public int GalleryId { get; set; }
        public Gallery Gallery { get; set; }
        public string Name { get; set; }
    }
}
