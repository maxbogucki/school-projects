namespace FinalExam_Bogucki.Services
{
    public class NewsService
    {
        private const string ApiKey = "";
        private const string BaseUrl = "https://newsapi.org/v2/top-headlines";

        private readonly HttpClient _httpClient;

        public NewsService()
        {
            _httpClient = new HttpClient();

            _httpClient.DefaultRequestHeaders.Add("Aceept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "FinalExam_Bogucki_App");
        }

        public async Task<string> GetNewsByCountry(string Country)
        {
            var url = $"{BaseUrl}?apiKey={ApiKey}&country={Country}";
            var response = await _httpClient.GetStringAsync(url);

            return response;
        }

        public async Task<string> GetNewsByCountryCategory(string Country, string Category)
        {
            var url = $"{BaseUrl}?apiKey={ApiKey}&country={Country}&category={Category}";
            var response = await _httpClient.GetStringAsync(url);

            return response;
        }
    }
}
