using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        public BudgetDBContext() { } // for design-time tools
        public BudgetDBContext(DbContextOptions<BudgetDBContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return; // <-- don’t override DI config

            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder.Path; 
            
            optionsBuilder.UseSqlite($"Data Source={System.IO.Path.Combine(localFolder, "budget.db")}");
            Debug.WriteLine($"DB Path: {Path.Combine(localFolder, "budget.db")}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.Property(u => u.Username).IsRequired().HasMaxLength(64);
                b.HasIndex(u => u.Username).IsUnique(); // enforce uniqueness
                b.HasOne(u => u.UserData)
                 .WithOne(d => d.User)
                 .HasForeignKey<UserData>(d => d.UserId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserData>(b =>
            {
                // decimal precision (note: SQLite stores as TEXT to preserve precision)
                b.Property(x => x.UserBudget).HasPrecision(18, 2);
                b.Property(x => x.UserBalance).HasPrecision(18, 2);

                b.HasMany(d => d.Expenses)
                 .WithOne(e => e.UserData)
                 .HasForeignKey(e => e.UserDataId)
                 .OnDelete(DeleteBehavior.Cascade);

                b.HasIndex(x => x.UserId).IsUnique(); // clarity (1:1 already creates unique)
            });

            modelBuilder.Entity<Expenses>(b =>
            {
                b.Property(e => e.Description).HasMaxLength(200);
                b.Property(e => e.Amount).HasPrecision(18, 2);
                // If you rename the property to OccurredAt, update this accordingly
                b.Property(e => e.DateTime)
                 .HasDefaultValueSql("CURRENT_TIMESTAMP"); // UTC on SQLite
                b.HasIndex(e => e.UserDataId);
            });
        }



    }
}
