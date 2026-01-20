using SQLite;
using Side_Hustle_Manager.Models;

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
            CategoryPicker.SelectedItem = value.Category;
        }
    }

    public AdminAddJobPage()
    {
        InitializeComponent();
        CategoryPicker.ItemsSource = JobCategories.All;
    }

    private async void Save_Clicked(object sender, EventArgs e)
    {
        bool isEdit = _sideHustle != null;

        if (_sideHustle == null)
            _sideHustle = new SideHustleModel();

        _sideHustle.Title = TitleEntry.Text ?? "";
        _sideHustle.Description = DescriptionEntry.Text ?? "";
        _sideHustle.Category = CategoryPicker.SelectedItem?.ToString() ?? "";
        _sideHustle.EmployerName = "Admin";
        _sideHustle.Pay = decimal.TryParse(PayEntry.Text, out var pay) ? pay : 0;

        await App.SideHustleDatabase.SaveSideHustleAsync(_sideHustle);

        await DisplayAlertAsync("Uspjesno",
            isEdit ? "Posao izmijenjen." : "Posao dodan.",
            "OK");

        // ?? RESET FORME KOD DODAVANJA
        if (!isEdit)
        {
            _sideHustle = null;
            TitleEntry.Text = "";
            DescriptionEntry.Text = "";
            PayEntry.Text = "";
            CategoryPicker.SelectedIndex = -1; // ?? OVO JE FALILO
        }
        else
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
