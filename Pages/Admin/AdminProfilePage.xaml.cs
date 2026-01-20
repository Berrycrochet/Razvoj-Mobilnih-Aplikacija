using Side_Hustle_Manager.Models;

namespace Side_Hustle_Manager.Pages.Admin;


public partial class AdminProfilePage : ContentPage
{
    public AdminProfilePage()
    {
        InitializeComponent();
    }

    private async void Logout_Clicked(object sender, EventArgs e)
    {
        bool answer = await DisplayAlertAsync("Logout", "Are you sure you want to logout?", "Yes", "No");
        if (answer)
        {
            Application.Current!.Windows[0].Page = new NavigationPage(new LoginPage());
        }
    }
}