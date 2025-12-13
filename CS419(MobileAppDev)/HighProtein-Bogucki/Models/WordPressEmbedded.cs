using System.Text.Json.Serialization;

namespace HighProtein.Models
{
    public class WordPressEmbedded
    {
        [JsonPropertyName("wp:featuredmedia")]
        public List<WordPressMedia>? FeaturedMedia { get; set; }
    }
}