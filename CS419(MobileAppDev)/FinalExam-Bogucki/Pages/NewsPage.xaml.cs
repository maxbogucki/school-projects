using FinalExam_Bogucki.Services;
using FinalExam_Bogucki.Models;

using System.Collections.ObjectModel;
using System.Text.Json;

using static FinalExam_Bogucki.App;

namespace FinalExam_Bogucki.Pages;

public partial class NewsPage : ContentPage
{
    public ObservableCollection<Article>? NewsArticles { get; set; }

    public Article? SelectedArticle { get; set; }
    public NewsPage()
    {
        InitializeComponent();
        Shell.SetTitleColor(this, Colors.White);
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        NewsCollectionView.IsVisible = false;
        NoNewsLabel.IsVisible = false;

        if (Preferences.ContainsKey("DestinationCity") && Preferences.ContainsKey("DestinationCountry"))
        {
            string DestinationCountry = Preferences.Get("DestinationCountry", string.Empty);
            string CountryCode = CountryCodesDictionary[DestinationCountry];
            string NewsCategory = Preferences.Get("NewsCategory", string.Empty);

            if (NewsCategory != string.Empty)
            {
                await LoadDataAsync(CountryCode, NewsCategory);
            }
            else
            {
                await LoadDataAsync(CountryCode, string.Empty);
            }
        }
        else
        {
            await Shell.Current.GoToAsync("..");
        }
    }

    private async void OnItemTapped(object sender, EventArgs e)
    {
        if (sender is Border border)
        {
            // Change the border color on tap
            border.Stroke = Colors.LightGrey;

            // Manually select the item (assuming sender has data bound to the item)
            if (sender is VisualElement { BindingContext: Article tappedItem })
            {
                // Set the SelectedItem manually
                SelectedArticle = tappedItem;

                // Ensure the selected article is not null
                if (SelectedArticle != null)
                {
                    // Navigate to the article page
                    string url = SelectedArticle.Url;

                    await Shell.Current.GoToAsync($"NewsDetailsPage?url={url}");
                }
            }
        }
    }

    public async Task LoadDataAsync(string Country, string Category = "")
    {
        NewsCollectionView.IsVisible = false;

        try
        {
            // Show the Activity Indicator
            BusyIndicator.IsRunning = true;
            BusyIndicator.IsVisible = true;

            NewsService ws = new NewsService();
            var response = string.Empty;

            if (Category != string.Empty)
            {
                response = await ws.GetNewsByCountryCategory(Country, Category);
            }
            else
            {
                response = await ws.GetNewsByCountry(Country);
            }

            if (response != null)
            {
                // Deserialize JSON response into your Article Response model
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var articleResponse = JsonSerializer.Deserialize<ArticleResponse>(response, options);

                // Bind to CollectionView
                if (articleResponse?.Articles != null && articleResponse.Articles.Count > 0)
                {
                    NewsCollectionView.ItemsSource = articleResponse.Articles;
                    NewsCollectionView.IsVisible = true;
                    NoNewsLabel.IsVisible = false;
                }
                else
                {
                    NoNewsLabel.Text = "\nThere is no news to show in this view.";
                    NoNewsLabel.IsVisible = true;
                    NewsCollectionView.IsVisible = false;
                }
            }
            else
            {
                NoNewsLabel.Text = "\nThere is no news to show in this view.";
                NoNewsLabel.IsVisible = true;
                NewsCollectionView.IsVisible = false;
            }
        }
        catch (Exception ex)
        {
            NoNewsLabel.Text = "\n" + ex.Message;
            NoNewsLabel.IsVisible = true;
            NewsCollectionView.IsVisible = false;
        }
        finally
        {
            // Hide the Activity Indicator
            BusyIndicator.IsRunning = false;
            BusyIndicator.IsVisible = false;
        }
    }
}