using HighProtein.Models;
using System.Text.Json;

namespace HighProtein.Services
{
    public class WordPressClient
    {
        private readonly HttpClient _http;
        private readonly string _baseUrl;

        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            ReadCommentHandling = JsonCommentHandling.Skip,
            AllowTrailingCommas = true
        };

        public WordPressClient(string baseUrl, HttpClient httpClient = null)
        {
            // Ensure no trailing slash
            _baseUrl = baseUrl.TrimEnd('/');

            _http = httpClient ?? new HttpClient();
        }

        /// <summary>
        /// Fetch WordPress posts with embedded media (featured images).
        /// </summary>
        /// <param name="page">Which page of results to fetch.</param>
        /// <param name="pageSize">How many posts per request.</param>
        /// <returns>List of WordPress posts.</returns>
        public async Task<List<WordPressPost>> GetPostsAsync(int page = 1, int pageSize = 10)
        {
            string url =
                $"{_baseUrl}/wp-json/wp/v2/posts?" +
                $"page={page}&per_page={pageSize}&_embed";

            HttpResponseMessage response = await _http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(
                    $"Failed to fetch posts: {(int)response.StatusCode} {response.ReasonPhrase}\n{error}"
                );
            }

            var json = await response.Content.ReadAsStringAsync();

            var posts = JsonSerializer.Deserialize<List<WordPressPost>>(json, _jsonOptions);

            return posts ?? new List<WordPressPost>();
        }

        /// <summary>
        /// Fetch a single post by ID, including featured media.
        /// </summary>
        public async Task<WordPressPost> GetPostByIdAsync(int id)
        {
            string url = $"{_baseUrl}/wp-json/wp/v2/posts/{id}?_embed";

            HttpResponseMessage response = await _http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException(
                    $"Failed to fetch post {id}: {(int)response.StatusCode} {response.ReasonPhrase}\n{error}"
                );
            }

            var json = await response.Content.ReadAsStringAsync();

            var post = JsonSerializer.Deserialize<WordPressPost>(json, _jsonOptions);

            return post;
        }

        /// <summary>
        /// Fetch posts filtered by a single category ID.
        /// </summary>
        public async Task<List<WordPressPost>> GetPostsByCategoryAsync(int categoryId, int page = 1, int pageSize = 10)
        {
            string url =
                $"{_baseUrl}/wp-json/wp/v2/posts?" +
                $"categories={categoryId}&page={page}&per_page={pageSize}&_embed";

            HttpResponseMessage response = await _http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new List<WordPressPost>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<WordPressPost>>(json, _jsonOptions);
        }

        /// <summary>
        /// Fetch posts filtered by tag ID.
        /// </summary>
        public async Task<List<WordPressPost>> GetPostsByTagAsync(int tagId, int page = 1, int pageSize = 10)
        {
            string url =
                $"{_baseUrl}/wp-json/wp/v2/posts?" +
                $"tags={tagId}&page={page}&per_page={pageSize}&_embed";

            HttpResponseMessage response = await _http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return new List<WordPressPost>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<WordPressPost>>(json, _jsonOptions);
        }
    }
}