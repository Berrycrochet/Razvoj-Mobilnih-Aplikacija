using SQLite;
using Side_Hustle_Manager.Models;
using System.Formats.Tar;

namespace Side_Hustle_Manager.Pages.Admin;

[QueryProperty(nameof(SideHustle), "SideHustle")]
public partial class AdminAddJobPage : ContentPage
{
    private SideHustleModel? _sideHustle;

    public SideHustleModel SideHustle
    {
        set
        {
            _sideHustle = value;

            TitleEntry.Text = value.Title;
            DescriptionEntry.Text = value.Description;
            PayEntry.Text = value.Pay.ToString();
        }
    }

    public AdminAddJobPage()
    {
        InitializeComponent();
    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
        if (_sideHustle == null)
        {
            _sideHustle = new SideHustleModel();
        }

        _sideHustle.Title = TitleEntry.Text ?? "";
        _sideHustle.Description = DescriptionEntry.Text ?? "";
        _sideHustle.Pay = decimal.TryParse(PayEntry.Text, out var pay) ? pay : 0;

        await App.SideHustleDatabase.SaveSideHustleAsync(_sideHustle);

        await DisplayAlertAsync("Uspješno.", "Posao saèuvan.", "OK");

        await Shell.Current.GoToAsync("..");
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
