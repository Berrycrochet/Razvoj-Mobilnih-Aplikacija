using Microsoft.Maui.Controls;
using SHM_ver1.Pages;
using SHM_ver1.Shells;
using SHM_ver1.Services;
using System.IO;

namespace SHM_ver1
{
    public partial class App : Application
    {
        // ---------------- Globalni pristup bazama ----------------
        public static UserDatabaseService UserDatabase { get; private set; }
        public static SideHustleDatabaseService SideHustleDatabase { get; private set; }

        public App()
        {
            InitializeComponent();

            // ---------------- Inicijalizacija baza ----------------
            // Tvoja baza (korisnici)
            UserDatabase = new UserDatabaseService();

            // Prijateljičina baza (Side Hustles + Applications)
            var shmDbPath = Path.Combine(FileSystem.AppDataDirectory, "shm.db3");
            SideHustleDatabase = new SideHustleDatabaseService(shmDbPath);

            // ---------------- Startna stranica ----------------
            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
