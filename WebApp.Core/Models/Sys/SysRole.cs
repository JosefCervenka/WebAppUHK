﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models.Base;

namespace WebApp.Core.Models.Sys
{
    [Index(nameof(Name), IsUnique = true)]
    public class SysRole : BaseEntity
    {

        [Required]
        public string Name { get; set; }
    }
}
