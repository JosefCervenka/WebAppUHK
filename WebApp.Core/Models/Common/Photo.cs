using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string Name { get; set; }

        [NotMapped]
        public string Url
        {
            get
            {
                return $"/api/image/{ImageId}";
            }
        }
    }
}
