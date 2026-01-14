using SHM_ver1.Models;
using SHM_ver1.Services;

namespace SHM_ver1.Pages
{
    public partial class RegisterPage : ContentPage
    {
        private DatabaseService _db;
        public UserModel RegisterData { get; set; }

        public RegisterPage()
        {
            InitializeComponent();
            _db = new DatabaseService();
            RegisterData = new UserModel();
            BindingContext = RegisterData;
        }
        private void OnUserChecked(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
                RegisterData.IsAdmin = false;
        }
        private void OnAdminChecked(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value)
                RegisterData.IsAdmin = true;
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(RegisterData.Username) || string.IsNullOrWhiteSpace(RegisterData.Password))
            {
                await DisplayAlert("Error", "Please fill all fields", "OK");
                return;
            }

            if (_db.UserExists(RegisterData.Username))
            {
                await DisplayAlert("Error", "User already exists", "OK");
                return;
            }

            
            RegisterData.IsAdmin = AdminRadio.IsChecked;
            _db.AddUser(RegisterData);

            await DisplayAlert("Success", "User registered!", "OK");
            await Navigation.PopAsync(); // vra?a na login
        }
    }
}