using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetTracker.Model
{
    public class Expenses
    {
        // Id for the table
        public int Id { get; set; }

        // Description
        public string Description { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public DateTime DateTime { get; set; }


        // Foreign and Navigation

        public int UserDataId { get; set; }

        public UserData UserData { get; set; } = null!;
        
    }
}
