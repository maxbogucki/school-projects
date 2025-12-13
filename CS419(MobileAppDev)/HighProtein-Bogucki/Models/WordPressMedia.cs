using System.Text.Json.Serialization;

namespace HighProtein.Models
{
    public class WordPressMedia
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("source_url")]
        public string? SourceUrl { get; set; }

        [JsonPropertyName("media_details")]
        public WordPressMediaDetails? MediaDetails { get; set; }
    }
}