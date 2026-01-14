using Microsoft.Extensions.DependencyInjection;
using SHM_ver1.Pages;
using SHM_ver1.Services;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHM_ver1
{
    public partial class App : Application
    {
        public static DatabaseService Database { get; private set; } = null!;
        public App()

        {
            InitializeComponent();
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "sidehustle.db3");

            // 🔥 DEV RESET (privremeno)
            if (File.Exists(dbPath))
            {
                File.Delete(dbPath);
            }

            Database = new DatabaseService(dbPath);
        
            
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new LoginPage());
        }
    }
}