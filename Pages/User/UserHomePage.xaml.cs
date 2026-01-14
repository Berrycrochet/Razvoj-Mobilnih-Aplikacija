using System.Collections.ObjectModel;
using SHM_ver1.Models;

namespace SHM_ver1.Pages.User;

public partial class UserHomePage : ContentPage
{
    public ObservableCollection<SideHustleModel> SideHustles { get; set; } = new();
    public UserHomePage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var hustles = await App.Database.GetSideHustlesAsync();
        SideHustles.Clear();
        foreach(var h in hustles)
        {
            SideHustles.Add(h);
        }
    }

    private async void Apply_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var job = button?.CommandParameter as SideHustleModel;

        if (job == null) return;

        var application = new JobApplicationModel
        {
            SideHustleId = job.Id,
            UserId = 1

        };

        await App.Database.ApplyToJobAsync(application);

        await DisplayAlertAsync("Prijavljen/a", "Prijavili ste se na posao.", "OK");
    }


}
