using System.Collections.ObjectModel;
using SHM_ver1.Models;

namespace SHM_ver1.Pages.Admin;

[QueryProperty(nameof(SideHustleId), "SideHustleId")]
public partial class AdminApplicationsPage : ContentPage
{
    public ObservableCollection<JobApplicationModel> Applications { get; set; } = new();

    public int SideHustleId { get; set; }

    public AdminApplicationsPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        Applications.Clear();
        var apps = await App.Database.GetApplicationsForJobAsync(SideHustleId);

        foreach (var a in apps)
            Applications.Add(a);
    }

    private async void Accept_Clicked(object sender, EventArgs e)
    {
        var app = (JobApplicationModel)((Button)sender).BindingContext;
        app.Status = "Accepted";

        await App.Database.SaveApplicationAsync(app);
        await DisplayAlertAsync("Done", "Application accepted", "OK");
    }

    private async void Reject_Clicked(object sender, EventArgs e)
    {
        var app = (JobApplicationModel)((Button)sender).BindingContext;
        app.Status = "Rejected";

        await App.Database.SaveApplicationAsync(app);
        await DisplayAlertAsync("Done", "Application rejected", "OK");
    }
}
