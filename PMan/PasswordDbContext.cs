using Microsoft.EntityFrameworkCore;
using PMan.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMan
{
    internal class PasswordDbContext : DbContext
    {
        public DbSet<UserEntity> PasswordEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure the database connection string here (e.g., SQLite or SQL Server).
            optionsBuilder.UseSqlite("Data Source=passwords.db");
        }


    }
}
