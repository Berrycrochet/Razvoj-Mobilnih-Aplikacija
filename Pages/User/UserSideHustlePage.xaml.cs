using System.Collections.ObjectModel;
using SHM_ver1.Models;

namespace SHM_ver1.Pages.User;

public partial class UserSideHustlePage : ContentPage
{
    public ObservableCollection<UserSideHustleViewModel> MySideHustles { get; set; } = new();

    public UserSideHustlePage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        MySideHustles.Clear();

        var data = await App.Database.GetUserSideHustlesAsync("1"); // fake user

        foreach (var item in data)
        {
            MySideHustles.Add(item);
        }
    }
}
