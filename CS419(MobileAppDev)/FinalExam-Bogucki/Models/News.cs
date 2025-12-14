using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace FinalExam_Bogucki.Models
{
    public class ArticleResponse
    {
        [JsonPropertyName("totalResults")]

        public required int TotalResults { get; set; }

        public ObservableCollection<Article>? Articles { get; set; }
    }

    public class Article
    {
        [JsonPropertyName("title")]

        public required string Title { get; set; }

        [JsonPropertyName("url")]

        public required string Url { get; set; }
    }
}
