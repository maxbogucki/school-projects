using FilmFinder_Bogucki.Models;
using FilmFinder_Bogucki.Services;

namespace FilmFinder_Bogucki.Pages;

public partial class DetailsPage : ContentPage
{
    private readonly MovieService _movieService;
    private readonly string _imdbId;

    public DetailsPage(string imdbId)
    {
        InitializeComponent();
        _movieService = new MovieService();
        _imdbId = imdbId;
        LoadMovieDetails();
    }

    private async void LoadMovieDetails()
    {
        try
        {
            var movieDetails = await _movieService.GetMovieDetailsAsync(_imdbId);

            if (movieDetails?.Response == "True")
            {
                DisplayMovieDetails(movieDetails);
            }
            else
            {
                await DisplayAlert("Error", movieDetails?.Error ?? "Failed to load movie details", "OK");
                await Navigation.PopAsync();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load details: {ex.Message}", "OK");
            await Navigation.PopAsync();
        }
        finally
        {
            LoadingIndicator.IsRunning = false;
            LoadingIndicator.IsVisible = false;
            DetailsContainer.IsVisible = true;
        }
    }

    private void DisplayMovieDetails(MovieDetails details)
    {
        // Basic Info
        TitleLabel.Text = details.Title;
        YearLabel.Text = details.Year;

        // Poster
        if (!string.IsNullOrEmpty(details.Poster) && details.Poster != "N/A")
        {
            PosterImage.Source = details.Poster;
        }

        // Plot
        PlotLabel.Text = !string.IsNullOrEmpty(details.Plot) && details.Plot != "N/A"
            ? details.Plot
            : "No plot available";

        // Credits
        DirectorLabel.Text = !string.IsNullOrEmpty(details.Director) && details.Director != "N/A"
            ? details.Director
            : "Unknown";

        WriterLabel.Text = !string.IsNullOrEmpty(details.Writer) && details.Writer != "N/A"
            ? details.Writer
            : "Unknown";

        StarsLabel.Text = !string.IsNullOrEmpty(details.Actors) && details.Actors != "N/A"
            ? details.Actors
            : "Unknown";

        // Additional Info
        RatedLabel.Text = !string.IsNullOrEmpty(details.Rated) && details.Rated != "N/A"
            ? details.Rated
            : "-";

        RuntimeLabel.Text = !string.IsNullOrEmpty(details.Runtime) && details.Runtime != "N/A"
            ? details.Runtime
            : "-";

        GenreLabel.Text = !string.IsNullOrEmpty(details.Genre) && details.Genre != "N/A"
            ? details.Genre
            : "-";

        ReleasedLabel.Text = !string.IsNullOrEmpty(details.Released) && details.Released != "N/A"
            ? details.Released
            : "-";
    }
}