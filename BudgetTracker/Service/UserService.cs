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
    public partial class UserService
    {
        // Normalize once (trim; make case-insensitive later if you add a NormalizedUsername column)
        private static string Normalize(string s) => (s ?? string.Empty).Trim();

        // Find OR create a user by username
        public static async Task<User> GetOrCreateAsync(string username, decimal monthlyBudget = 0m)
        {
            var name = Normalize(username);
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Username cannot be empty.", nameof(username));

            await using var db = new BudgetDBContext();

            var existing = await db.User
                .Include(u => u.UserData)
                .SingleOrDefaultAsync(u => u.Username == name);

            if (existing != null) return existing;

            var user = new User
            {
                Username = name,
                UserData = new UserData
                {
                    UserBudget = monthlyBudget,
                    UserBalance = monthlyBudget
                }
            };

            db.User.Add(user);

            try
            {
                await db.SaveChangesAsync();
                return user;
            }
            catch (DbUpdateException)
            {
                // If a unique index exists and two callers race, re-query and return the winner.
                return await db.User
                    .Include(u => u.UserData)
                    .SingleAsync(u => u.Username == name);
            }
        }

        // Strict create (throws if username exists)
        public static async Task<User> CreateAsync(string username, decimal monthlyBudget = 0m)
        {
            var name = Normalize(username);
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Username cannot be empty.", nameof(username));

            await using var db = new BudgetDBContext();

            var exists = await db.User.AnyAsync(u => u.Username == name);
            if (exists) throw new InvalidOperationException("Username already exists.");

            var user = new User
            {
                Username = name,
                UserData = new UserData
                {
                    UserBudget = monthlyBudget,
                    UserBalance = monthlyBudget
                }
            };

            db.User.Add(user);
            await db.SaveChangesAsync();
            return user;
        }

        // Read by name (choose whether to include expenses)
        public static async Task<User?> GetByNameAsync(
            string username,
            bool includeExpenses = false,
            bool asNoTracking = true)
            {
                var name = (username ?? "").Trim();
                await using var db = new BudgetDBContext();

                IQueryable<User> q = db.User;

                // Build the include(s)
                if (includeExpenses)
                {
                    q = q.Include(u => u.UserData)
                         .ThenInclude(ud => ud.Expenses);
                }
                else
                {
                    q = q.Include(u => u.UserData);
                }

                if (asNoTracking)
                    q = q.AsNoTracking();

                // Optional: avoid cartesian explosion with multiple includes
                q = q.AsSplitQuery();

                return await q.SingleOrDefaultAsync(u => u.Username == name);
            }
    }
}
