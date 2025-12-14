using HighProtein.Models;
using HighProtein.Services;
using HighProtein_Bogucki.Models;
using static HighProtein_Bogucki.App;

namespace HighProtein_Bogucki.Pages;

public partial class DinnerPage : ContentPage
{
    public WordPressPost? SelectedRecipe { get; set; }

    public DinnerPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadPostDataAsync((int)RECIPES.DINNER);
    }

    private async void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (sender is Border border)
        {
            border.Stroke = Colors.LightGray;
            if (sender is VisualElement { BindingContext: WordPressPost tappedItem })
            {
                SelectedRecipe = tappedItem;
                if (SelectedRecipe != null)
                {
                    string? url = SelectedRecipe.Link;
                    await Shell.Current.GoToAsync($"RecipeDetailsPage?url={url}");
                }
            }
        }
    }

    private async void LoadPostDataAsync(int Category)
    {
        RecipesCollectionView.IsVisible = false;
        try
        {
            BusyIndicator.IsRunning = true;
            BusyIndicator.IsVisible = true;
            var wp = new WordPressClient("https://wp.elmhurst.edu/highprotein");
            var posts = await wp.GetPostsByCategoryAsync(Category);
            // Sort
            posts.Sort((a, b) => string.Compare(a?.Title?.Rendered, b?.Title?.Rendered, StringComparison.OrdinalIgnoreCase));
            if (posts != null)
            {
                RecipesCollectionView.ItemsSource = posts;
                RecipesCollectionView.IsVisible = true;
                NoRecipes.IsVisible = false;
            }
            else
            {
                NoRecipes.Text = "There are no recipes to show in this view.";
                NoRecipes.IsVisible = true;
                RecipesCollectionView.IsVisible = false;
            }
        }
        catch (Exception ex)
        {
            NoRecipes.Text = ex.Message;
            NoRecipes.IsVisible = true;
            RecipesCollectionView.IsVisible = false;
        }
        finally
        {
            BusyIndicator.IsRunning = false;
            BusyIndicator.IsVisible = false;
        }
    }

    private async void OnHomeClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}