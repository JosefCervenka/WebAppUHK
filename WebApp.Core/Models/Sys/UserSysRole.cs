using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models.Base;

namespace WebApp.Core.Models.Sys
{
    public class UserSysRole : BaseEntity
    {
        public User User { get; set; }

        [Required]
        public int UserId { get; set; }

        public SysRole SysRole { get; set; }

        [Required]
        public int SysRoleId { get; set; }
    }
}
