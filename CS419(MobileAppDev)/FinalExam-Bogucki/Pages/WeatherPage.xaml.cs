using FinalExam_Bogucki.Services;
using static FinalExam_Bogucki.App;

namespace FinalExam_Bogucki.Pages;

public partial class WeatherPage : ContentPage
{
	public WeatherPage()
	{
		InitializeComponent();
		Shell.SetTitleColor(this, Colors.White);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        BusyIndicator.IsVisible = true;
        BusyIndicator.IsRunning = true;
        WeatherLabel.IsVisible = false;

        if (!Preferences.ContainsKey("DestinationCity"))
        {
            return;
        }

        string city = Preferences.Get("DestinationCity", string.Empty);

        bool hasLat = LatitudeDictionary.TryGetValue(city, out double latitude);
        bool hasLon = LongitudeDictionary.TryGetValue(city, out double longitude);

        await GetWeatherData(latitude, longitude);
    }


    private async Task GetWeatherData(double latitude, double longitude)
	{
        // Show the ActivityIndicator when the page starts loading
		BusyIndicator.IsRunning = true;
		BusyIndicator.IsVisible = true;

		WeatherService ws = new();

		try
		{
			var response = await ws.GetWeatherByLatLon(latitude, longitude);
			if(response != null)
			{
				WeatherLabel.Text = $"Weather in {response.Name}:\n" +
									$"Temp: {response?.Main?.Temp}°F\n" +
									$"Humidity: {response?.Main?.Humidity}%\n" +
									$"Condition: {response?.Weather?[0]?.Description}";
            }
            else
            {
                WeatherLabel.Text = "Weather data not found.";
            }
        } catch (Exception ex)
		{
			WeatherLabel.Text = $"Error: {ex.Message}";
		} finally
		{
			BusyIndicator.IsRunning = false;
			BusyIndicator.IsVisible = false;
			WeatherLabel.IsVisible = true;
        }
    }
}