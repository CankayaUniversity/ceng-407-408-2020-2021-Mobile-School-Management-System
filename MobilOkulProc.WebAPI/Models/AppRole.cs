﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobilOkulProc.WebAPI.Models
{
    public class AppRole : IdentityRole<int>
    {
        public string Admin = "Admin";
        public string Student = "Student";
        public string Teacher = "Teacher";

    }
}
