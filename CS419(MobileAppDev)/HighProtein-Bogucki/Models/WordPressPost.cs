using System.Text.Json.Serialization;

namespace HighProtein.Models
{
    public class WordPressPost
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("link")]
        public string? Link { get; set; }

        [JsonPropertyName("title")]
        public RenderedText? Title { get; set; }

        [JsonPropertyName("categories")]
        public int[]? Categories { get; set; }

        [JsonPropertyName("tags")]
        public int[]? Tags { get; set; }

        // WordPress includes embedded objects only if you request ?_embed
        [JsonPropertyName("_embedded")]
        public WordPressEmbedded? Embedded { get; set; }

        /// <summary>
        /// Convenience property: featured image URL (if available)
        /// </summary>
        public string? FeaturedImageUrl =>
            Embedded?.FeaturedMedia?
                .FirstOrDefault()?
                .MediaDetails?
                .Sizes?
                .Large?.SourceUrl
            ?? Embedded?.FeaturedMedia?
                .FirstOrDefault()?
                .SourceUrl;
    }
}