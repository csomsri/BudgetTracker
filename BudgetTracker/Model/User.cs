using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.Model
{
    class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;

        // Navigation
        public UserData UserData { get; set; } = new UserData();
    }
}
