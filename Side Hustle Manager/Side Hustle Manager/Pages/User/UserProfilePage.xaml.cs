using Microsoft.Maui.Devices.Sensors; // za Geolocation
using Microsoft.Maui.ApplicationModel; // za permissions
using Microsoft.Maui.Essentials;


namespace Side_Hustle_Manager.Pages.User 
{

    public partial class UserProfilePage : ContentPage
    {
        public UserProfilePage()
        {
            InitializeComponent();
        }

        // Logout dugme

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlertAsync("Odjava", "Da li ste sigurni da se želite odjaviti?", "Da", "Ne");
            if (answer)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }




        private async void OnGetLocationClicked(object sender, EventArgs e)
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
                var location = await Geolocation.Default.GetLocationAsync(request);

                if (location != null)
                {
                    // Reverse geocoding
                    var placemarks = await Geocoding.Default.GetPlacemarksAsync(location.Latitude, location.Longitude);
                    var placemark = placemarks?.FirstOrDefault();

                    if (placemark != null)
                    {
                        string city = placemark.Locality; // ovo je grad
                        string country = placemark.CountryName;
                        LocationLabel.Text = $"Grad: {city}, Država: {country}";
                    }
                    else
                    {
                        LocationLabel.Text = "Grad nije pronaðren";
                    }
                }
                else
                {
                    LocationLabel.Text = "Nije moguæe dobiti lokaciju.";
                }
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlertAsync("Greška", "Ova funkcija nije podržana na ovom ureðaju.", "OK");
            }
            catch (FeatureNotEnabledException)
            {
                await DisplayAlertAsync("Greška", "Molimo omoguæite usluge lokacije.", "OK");
            }
            catch (PermissionException)
            {
                await DisplayAlertAsync("Greška", "Dozvola za lokaciju je odbijena.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Greška", $"Došlo je do greške: {ex.Message}", "OK");
            }
        }



    }
}

