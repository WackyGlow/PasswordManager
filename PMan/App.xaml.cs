using Microsoft.EntityFrameworkCore;
using System.IO;

namespace PMan
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}