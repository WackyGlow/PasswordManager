using Microsoft.EntityFrameworkCore;
using PMan.Infrastructure;
using System.IO;

namespace PMan
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "passwords.db");
            var options = new DbContextOptionsBuilder<PasswordManagerDbContext>()
                .UseSqlite($"Filename={dbPath}")
                .Options;

            using var dbContext = new PasswordManagerDbContext(options);
            dbContext.Database.EnsureCreated();



            MainPage = new AppShell();
        }
    }
}