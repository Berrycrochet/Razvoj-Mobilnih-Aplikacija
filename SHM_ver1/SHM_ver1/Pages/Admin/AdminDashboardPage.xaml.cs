namespace SHM_ver1.Pages.Admin;

public partial class AdminDashboardPage : ContentPage
{
	public AdminDashboardPage()
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


