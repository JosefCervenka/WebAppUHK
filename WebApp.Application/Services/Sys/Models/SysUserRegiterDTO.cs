﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Application.Services.Sys.Models
{
    public class SysUserRegiterDTO
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public string PasswordAgain { get; set; }

        public string Email { get; set; }
    }
}
