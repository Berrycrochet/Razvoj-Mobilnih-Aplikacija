using Microsoft.Maui.Controls;
using SHM_ver1.Pages;
using SHM_ver1.Shells;

namespace SHM_ver1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Startna stranica je LoginPage
            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
