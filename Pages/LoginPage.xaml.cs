using SHM_ver1.Models;
using SHM_ver1.Shells;
using System.Security.Cryptography.X509Certificates;

namespace SHM_ver1.Pages;

public partial class LoginPage : ContentPage
{
    public LoginModel LoginData { get; set; }

    public LoginPage()
	{
		InitializeComponent();

		LoginData = new LoginModel();
		BindingContext = LoginData;
	}

	private async void OnLoginClicked(object sender, EventArgs e)
	{
		var username = LoginData?.Username?.Trim();
		if (username == "user")
		{
			Application.Current!.Windows[0].Page= new UserShell();
		}
		else if (username == "admin")
		{
			Application.Current!.Windows[0].Page = new AdminShell();
		}
		else
		{
			await DisplayAlertAsync("Error", "Invalid Login", "OK");
		}
	}


}
