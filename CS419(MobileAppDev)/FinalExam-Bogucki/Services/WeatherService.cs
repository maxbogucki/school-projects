using FinalExam_Bogucki.Models;
using System.Text.Json;

namespace FinalExam_Bogucki.Services
{
    public class WeatherService
    {
        private const string ApiKey = "";
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";

        private readonly HttpClient _httpClient;

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<WeatherResponse?> GetWeatherByLatLon(double latitude, double longitude)
        {
            var url = $"{BaseUrl}?lat={latitude}&lon={longitude}&appid={ApiKey}&units=imperial";
            var response = await _httpClient.GetStringAsync(url);
            return JsonSerializer.Deserialize<WeatherResponse>(response);
        }
    }
}
