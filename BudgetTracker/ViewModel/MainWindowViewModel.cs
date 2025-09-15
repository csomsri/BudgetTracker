using CommunityToolkit.Mvvm.Input;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using BudgetTracker.DataBase;
using Microsoft.UI.Xaml.Media.Animation;


using BudgetTracker.Model;   // <-- for User/UserData
using System.Linq;

namespace BudgetTracker.ViewModel
{
    public partial class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
#if DEBUG
            using var db = new BudgetDBContext(); 
            db.Database.EnsureCreated();

            if (!db.User.Any())
            {
                var testUser = new User
                {
                    Username = "TestUser",
                    UserData = new UserData
                    {
                        UserBudget = 0,
                        UserBalance = 0
                    }
                };

                db.User.Add(testUser);
                db.SaveChanges();
            }
#endif
        }
    }
}


