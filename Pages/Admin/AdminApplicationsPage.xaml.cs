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

        var apps = await App.Database.GetApplicationsForJobAsync(SideHustleId);

        Applications.Clear();
        foreach (var a in apps)
        {
            Applications.Add(a);
        }
    }
}
