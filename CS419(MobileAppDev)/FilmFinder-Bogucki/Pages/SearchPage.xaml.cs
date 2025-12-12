using FilmFinder_Bogucki.Models;
using FilmFinder_Bogucki.Services;
using System.Collections.ObjectModel;
using System.Formats.Tar;

namespace FilmFinder_Bogucki.Pages;

public partial class SearchPage : ContentPage
{
    private readonly MovieService _movieService;
    private ObservableCollection<Movie> _movies;

    public SearchPage()
    {
        InitializeComponent();
        _movieService = new MovieService();
        _movies = new ObservableCollection<Movie>();
        MoviesCollectionView.ItemsSource = _movies;
    }

    private async void OnSearchClicked(object sender, EventArgs e)
    {
        string title = TitleEntry.Text?.Trim();
        string year = YearEntry.Text?.Trim();

        if (string.IsNullOrEmpty(title))
        {
            await DisplayAlert("Error", "Please enter a movie title", "OK");
            return;
        }

        StatusLabel.Text = "Searching...";
        StatusLabel.IsVisible = true;
        SearchButton.IsEnabled = false;
        _movies.Clear();

        try
        {
            var searchResult = await _movieService.SearchMoviesAsync(title, year);

            if (searchResult?.Response == "True" && searchResult.Search != null)
            {
                foreach (var movie in searchResult.Search)
                {
                    _movies.Add(movie);
                }
                StatusLabel.Text = $"Found {_movies.Count} result(s)";
            }
            else
            {
                StatusLabel.Text = searchResult?.Error ?? "No movies found";
            }
        }
        catch (Exception ex)
        {
            StatusLabel.Text = "Error searching movies";
            await DisplayAlert("Error", $"Failed to search: {ex.Message}", "OK");
        }
        finally
        {
            SearchButton.IsEnabled = true;
        }
    }

    private async void OnMovieSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Movie selectedMovie)
        {
            // Navigate to details page with imdbID
            await Navigation.PushAsync(new DetailsPage(selectedMovie.ImdbID ?? ""));

            // Deselect the item
            MoviesCollectionView.SelectedItem = null;
        }
    }
}