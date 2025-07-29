using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.Model
{
    class UserData
    {
        // Id For the user
        public int Id { get; set; }
        public decimal UserBudget { get; set; }

        public decimal UserBalance { get; set; }

        // Navigation and Foreign Keys
        public int UserID { get; set; }

        public User User { get; set; } = new User();

        // Connection To Expenses

        public List<Expenses> Expenses { get; set; } = new List<Expenses>();



    }
}
