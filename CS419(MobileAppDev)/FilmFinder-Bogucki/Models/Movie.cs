namespace FilmFinder_Bogucki.Models
{
    public class Movie
    {
        public string? Title { get; set; }
        public string? Year { get; set; }
        public string? ImdbID { get; set; }
        public string? Type { get; set; }
        public string? Poster { get; set; }
    }

    public class SearchResult
    {
        public string? Response { get; set; }
        public List<Movie>? Search { get; set; }
        public string? totalResults { get; set; }
        public string? Error { get; set; }
    }
}