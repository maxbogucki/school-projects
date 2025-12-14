using System.Text.Json.Serialization;

namespace FinalExam_Bogucki.Models
{
    public class WeatherResponse
    {
        [JsonPropertyName("main")]
        public MainWeather? Main { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("weather")]

        public WeatherDescription[]? Weather { get; set; }
    }

    public class MainWeather
    {
        [JsonPropertyName("temp")]
        public double Temp { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }

    public class WeatherDescription
    {
        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}
