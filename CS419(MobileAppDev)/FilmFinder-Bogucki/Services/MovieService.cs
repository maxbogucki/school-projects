using System.Text.Json;
using FilmFinder_Bogucki.Models;

namespace FilmFinder_Bogucki.Services
{
    public class MovieService
    {
        private const string API_KEY = "283ae1cb";
        private const string BASE_URL = "https://www.omdbapi.com/";
        private readonly HttpClient _httpClient;

        public MovieService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<SearchResult> SearchMoviesAsync(string title, string? year = null)
        {
            try
            {
                string url = $"{BASE_URL}?apikey={API_KEY}&t={Uri.EscapeDataString(title)}";

                if (!string.IsNullOrEmpty(year))
                {
                    url += $"&y={year}";
                }

                Console.WriteLine($"API URL: {url}"); // Debug output
                string response = await _httpClient.GetStringAsync(url);
                Console.WriteLine($"API Response: {response}"); // Debug output

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                // Single movie result - wrap in SearchResult
                var movieDetails = JsonSerializer.Deserialize<MovieDetails>(response, options);

                if (movieDetails?.Response == "True")
                {
                    // Convert single movie to search result list
                    var movie = new Movie
                    {
                        Title = movieDetails.Title,
                        Year = movieDetails.Year,
                        ImdbID = movieDetails.ImdbID,
                        Type = movieDetails.Type,
                        Poster = movieDetails.Poster
                    };

                    return new SearchResult
                    {
                        Response = "True",
                        Search = new List<Movie> { movie },
                        totalResults = "1"
                    };
                }
                else
                {
                    return new SearchResult
                    {
                        Response = "False",
                        Error = movieDetails?.Error ?? "Movie not found"
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching movies: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return new SearchResult
                {
                    Response = "False",
                    Error = $"Failed to search movies: {ex.Message}"
                };
            }
        }

        public async Task<MovieDetails> GetMovieDetailsAsync(string imdbId)
        {
            try
            {
                string url = $"{BASE_URL}?apikey={API_KEY}&i={imdbId}&plot=full";

                Console.WriteLine($"API URL: {url}"); // Debug output
                string response = await _httpClient.GetStringAsync(url);
                Console.WriteLine($"API Response: {response}"); // Debug output

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var details = JsonSerializer.Deserialize<MovieDetails>(response, options);

                return details ?? new MovieDetails { Response = "False", Error = "No data returned" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting movie details: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return new MovieDetails
                {
                    Response = "False",
                    Error = $"Failed to load movie details: {ex.Message}"
                };
            }
        }
    }
}