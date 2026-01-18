namespace SHM_ver1.Pages.User;

public partial class UserProfilePage : ContentPage
{
	public UserProfilePage()
	{
		InitializeComponent();
	}

	private void Logout_Clicked(object sender, EventArgs e)
	{
		Application.Current!.Windows[0].Page = new LoginPage();
	}
}