using Side_Hustle_Manager.Models;
using System.Collections.ObjectModel;

namespace Side_Hustle_Manager.Pages.User;

public partial class UserHomePage : ContentPage
{
    public ObservableCollection<SideHustleModel> SideHustles { get; set; } = new();

    private List<SideHustleModel> _allSideHustles = new();

    public UserHomePage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var hustles = await App.SideHustleDatabase.GetSideHustlesAsync();

        _allSideHustles = hustles;   // 🔥 OVO JE FALILO

        SideHustles.Clear();
        foreach (var h in hustles)
            SideHustles.Add(h);

        LoadCategories(); // 🔥 OVO JE FALILO
    }

    private async void Apply_Clicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var job = button?.CommandParameter as SideHustleModel;

        if (job == null) return;

        var application = new JobApplicationModel
        {
            SideHustleId = job.Id,
            UserId = "1"
        };

        await App.SideHustleDatabase.ApplyToJobAsync(application);

        await DisplayAlertAsync("Prijavljen/a", "Prijavili ste se na posao.", "OK");
    }

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
}
