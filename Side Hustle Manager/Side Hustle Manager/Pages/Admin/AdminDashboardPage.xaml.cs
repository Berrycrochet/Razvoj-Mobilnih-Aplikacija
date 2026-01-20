using Side_Hustle_Manager.Models;
using System.Collections.ObjectModel;

namespace Side_Hustle_Manager.Pages.Admin;


public partial class AdminDashboardPage : ContentPage
{
    public ObservableCollection<SideHustleModel> SideHustles { get; set; } = new();

    public AdminDashboardPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var hustles = await App.SideHustleDatabase.GetSideHustlesAsync();

        SideHustles.Clear();
        foreach (var h in hustles)
        {
            SideHustles.Add(h);
        }
    }

    private async void Delete_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var hustle = button?.CommandParameter as SideHustleModel;

        if (hustle == null) return;

        bool confirm = await DisplayAlertAsync(
            "Izbriši",
            $"Izbriši '{hustle.Title}'?",
            "Da",
            "Odustani");

        if (!confirm) return;

        await App.SideHustleDatabase.DeleteSideHustleAsync(hustle);

        SideHustles.Remove(hustle);
    }

    private async void Edit_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var hustle = button?.CommandParameter as SideHustleModel;

        if (hustle == null) return;

        await Shell.Current.GoToAsync(nameof(AdminAddJobPage), true,
            new Dictionary<string, object>
            {
                { "SideHustle", hustle }
            });
    }

    private async void Applications_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var job = button?.CommandParameter as SideHustleModel;

        if (job == null) return;

        await Shell.Current.GoToAsync(nameof(AdminApplicationPage),
            new Dictionary<string, object>
            {
            { "SideHustleId", job.Id }
            });
    }

}
