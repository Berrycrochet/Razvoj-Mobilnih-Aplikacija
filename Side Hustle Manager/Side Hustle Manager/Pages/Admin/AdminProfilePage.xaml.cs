using Side_Hustle_Manager.Models;

namespace Side_Hustle_Manager.Pages.Admin;


public partial class AdminProfilePage : ContentPage
{
    public AdminProfilePage()
    {
        InitializeComponent();
    }

    private async void OnLogoutClicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
        if (answer)
        {
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
    }
}