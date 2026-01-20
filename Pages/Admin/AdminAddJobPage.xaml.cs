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
            CategoryEntry.Text = value.Category;
        }
    }

    public AdminAddJobPage()
    {
        InitializeComponent();
    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
        bool isEdit = _sideHustle != null;

        if (_sideHustle == null)
            _sideHustle = new SideHustleModel();

        _sideHustle.Title = TitleEntry.Text ?? "";
        _sideHustle.Description = DescriptionEntry.Text ?? "";
        _sideHustle.Category = CategoryEntry.Text ?? "";
        _sideHustle.Pay = decimal.TryParse(PayEntry.Text, out var pay) ? pay : 0;

        await App.SideHustleDatabase.SaveSideHustleAsync(_sideHustle);

        await DisplayAlertAsync("Uspješno",
            isEdit ? "Posao izmijenjen." : "Posao dodan.",
            "OK");

        // ?? KLJUÈNO: reset forme nakon ADD-a
        if (!isEdit)
        {
            _sideHustle = null;
            TitleEntry.Text = "";
            DescriptionEntry.Text = "";
            CategoryEntry.Text = "";
            PayEntry.Text = "";
        }
        else
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
