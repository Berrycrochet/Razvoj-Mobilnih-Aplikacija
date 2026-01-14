using SHM_ver1.Models;
using SHM_ver1.Services;
using SHM_ver1.Shells;

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
    private DatabaseService _db = new DatabaseService();

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var user = _db.GetUser(LoginData.Username, LoginData.Password);

        if (user != null)
        {
            if (user.IsAdmin)
                Application.Current.MainPage = new AdminShell();
            else
                Application.Current.MainPage = new UserShell();
        }
        else
        {
            await DisplayAlert("Error", "Invalid username or password", "OK");
        }
    }

    private async void OnRegisterClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
}

