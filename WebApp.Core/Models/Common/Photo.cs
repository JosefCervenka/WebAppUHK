using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models.Base;
using WebApp.Core.Models.Sys;

namespace WebApp.Core.Models.Common
{
    public class Photo : BaseEntity
    {
        public string Name { get; set; }
        public byte[] Image { get; set; }
    }
}
