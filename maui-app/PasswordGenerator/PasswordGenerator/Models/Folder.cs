﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordGenerator.Models
{
    public class Folder
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
