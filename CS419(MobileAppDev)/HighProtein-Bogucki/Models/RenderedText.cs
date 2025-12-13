using System.Text.Json.Serialization;

namespace HighProtein.Models
{
    public class RenderedText
    {
        [JsonPropertyName("rendered")]
        public string? Rendered { get; set; }
    }
}