using BudgetTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BudgetTracker.DataBase;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Service
{
    public class UserService
    {
        public static User CreatedUser(string username)
        {
            using var db = new BudgetDBContext();
            var user = new User
            {
                Username = username,
                UserData = new UserData
                {

                    UserBudget = 0,
                    UserBalance = 0,

                }

            };

            db.User.Add(user);
            db.SaveChanges();
            return user;
        }

        public static User? GetUserByName(string username)
        {

            using var db = new BudgetDBContext();

            return db.User
                .Include(u => u.UserData)
                .ThenInclude(ud => ud.Expenses)
                .FirstOrDefault(u => u.Username == username);

        }

    }
}
