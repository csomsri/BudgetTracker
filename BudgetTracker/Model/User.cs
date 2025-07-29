﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;

        // Navigation
        public UserData UserData { get; set; } = null!;
    }
}
