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

            if (!string.IsNullOrEmpty(value.ImagePath))
            {
                JobImage.Source = value.ImagePath;
                JobImage.IsVisible = true;
            }
        }
    }

    public AdminAddJobPage()
    {
        InitializeComponent();
        CategoryPicker.ItemsSource = JobCategories.All;
    }

    private async void PickImage_Clicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Izaberi sliku posla",
            FileTypes = FilePickerFileType.Images
        });

        if (result == null) return;

        JobImage.Source = result.FullPath;
        JobImage.IsVisible = true;

        if (_sideHustle == null)
            _sideHustle = new SideHustleModel();

        _sideHustle.ImagePath = result.FullPath;
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

        await DisplayAlert("Uspješno", "Posao saèuvan", "OK");
        await Shell.Current.GoToAsync("..");
    }
}
