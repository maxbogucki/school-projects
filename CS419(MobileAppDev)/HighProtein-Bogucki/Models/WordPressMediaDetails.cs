using System.Text.Json.Serialization;

namespace HighProtein.Models
{
    public class WordPressMediaDetails
    {
        [JsonPropertyName("sizes")]
        public WordPressMediaSizes? Sizes { get; set; }
    }

    public class WordPressMediaSizes
    {
        [JsonPropertyName("thumbnail")]
        public WordPressMediaSize? Thumbnail { get; set; }

        [JsonPropertyName("medium")]
        public WordPressMediaSize? Medium { get; set; }

        [JsonPropertyName("large")]
        public WordPressMediaSize? Large { get; set; }

        // fallback in case WP theme registers custom size names
        [JsonExtensionData]
        public Dictionary<string, object>? AdditionalSizes { get; set; }
    }

    public class WordPressMediaSize
    {
        [JsonPropertyName("source_url")]
        public string? SourceUrl { get; set; }
    }
}