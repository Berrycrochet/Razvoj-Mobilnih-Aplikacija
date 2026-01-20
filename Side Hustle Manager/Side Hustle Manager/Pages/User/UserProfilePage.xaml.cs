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
            bool answer = await DisplayAlert("Logout", "Are you sure you want to logout?", "Yes", "No");
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
                        LocationLabel.Text = $"City: {city}, Country: {country}";
                    }
                    else
                    {
                        LocationLabel.Text = "City not found";
                    }
                }
                else
                {
                    LocationLabel.Text = "Unable to get location";
                }
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Error", "Feature not supported on this device", "OK");
            }
            catch (FeatureNotEnabledException)
            {
                await DisplayAlert("Error", "Please enable location services", "OK");
            }
            catch (PermissionException)
            {
                await DisplayAlert("Error", "Location permission denied", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Something went wrong: {ex.Message}", "OK");
            }
        }



    }
}

