﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
   public class User
    {
        public string UserName { get; set; } //user name
        public string Password { get; set; } //password
        public bool AdminAccess { get; set; } //access for admin
        public bool IsDeleted { get; set; } //check if the user has deleted
    }
}
