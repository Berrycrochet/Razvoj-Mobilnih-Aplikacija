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

        var sideHustles = await App.SideHustleDatabase.GetUserSideHustlesAsync("1"); // vra?a List<UserSideHustleViewModel>

        foreach (var item in sideHustles)
        {
            MySideHustles.Add(item);
        }
    }
}
