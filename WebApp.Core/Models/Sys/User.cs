using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Core.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebApp.Core.Models.Sys
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Name), IsUnique = true)]
    public class User : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [JsonIgnore]
        public string Email { get; set; }

        [Required]
        [JsonIgnore]
        public string PasswordHash { get; set; }

        [Required]
        [JsonIgnore]
        public byte[] Salt { get; set; }

        public List<UserSysRole> UserSysRoles { get; set; }
    }
}
