namespace SHM_ver1.Pages.Admin;

public partial class AdminProfilePage : ContentPage
{
	public AdminProfilePage()
	{
		InitializeComponent();
	}

    private void Logout_Clicked(object sender, EventArgs e)
    {
        Application.Current!.Windows[0].Page = new LoginPage();
    }
}