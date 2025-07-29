using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BudgetTracker.Model;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.DataBase
{
    public partial class BudgetDBContext : DbContext
    {
        public DbSet<User> User { get; set; } 

        public DbSet<UserData> UserDatas { get; set; }
        
        public DbSet<Expenses> Expenses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder.Path; 
            
            optionsBuilder.UseSqlite($"Data Source={System.IO.Path.Combine(localFolder, "budget.db")}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserData)
                .WithOne(d => d.User)
                .HasForeignKey<UserData>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserData>()
                .HasMany(d => d.Expenses)
                .WithOne(e => e.UserData)
                .HasForeignKey(e => e.UserDataId)
                .OnDelete(DeleteBehavior.Cascade);
        }
         
       
    }
}
