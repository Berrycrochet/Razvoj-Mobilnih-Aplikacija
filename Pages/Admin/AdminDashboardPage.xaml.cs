using Side_Hustle_Manager.Models;
using System.Collections.ObjectModel;

namespace Side_Hustle_Manager.Pages.Admin;

public partial class AdminDashboardPage : ContentPage
{
    public ObservableCollection<SideHustleModel> SideHustles { get; set; } = new();

    private List<SideHustleModel> _allSideHustles = new();

    public AdminDashboardPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var hustles = await App.SideHustleDatabase.GetSideHustlesAsync();

        _allSideHustles = hustles;

        SideHustles.Clear();
        foreach (var h in hustles)
            SideHustles.Add(h);

        LoadCategories();
    }

    // 🔽 UČITAVANJE KATEGORIJA
    private void LoadCategories()
    {
        var categories = _allSideHustles
            .Select(h => h.Category)
            .Where(c => !string.IsNullOrWhiteSpace(c))
            .Distinct()
            .OrderBy(c => c)
            .ToList();

        categories.Insert(0, "Sve");

        CategoryPicker.ItemsSource = categories;
        CategoryPicker.SelectedIndex = 0;
    }

    // 🔍 FILTER
    private void ApplyFilter(string category)
    {
        SideHustles.Clear();

        var filtered = category == "Sve"
            ? _allSideHustles
            : _allSideHustles.Where(h => h.Category == category);

        foreach (var h in filtered)
            SideHustles.Add(h);
    }

    private void CategoryPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CategoryPicker.SelectedItem == null) return;

        ApplyFilter(CategoryPicker.SelectedItem.ToString()!);
    }

    // 🗑 DELETE
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

        _allSideHustles.Remove(hustle);
        SideHustles.Remove(hustle);
    }

    // ✏ EDIT
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

    // 📄 APPLICATIONS
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
