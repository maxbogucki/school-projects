using HighProtein.Models;
using HighProtein.Services;
using HighProtein_Bogucki.Models;
using static HighProtein_Bogucki.App;

namespace HighProtein_Bogucki
{
    public partial class MainPage : ContentPage
    {
        public Carousel? SelectedRecipe { get; set; }
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetFeaturedPosts();
        }

        private async void GetFeaturedPosts()
        {
            var wp = new WordPressClient("https://wp.elmhurst.edu/highprotein");
            var posts = await wp.GetPostsByTagAsync((int)RECIPES.FEATURED, 1, 100);

            // Sort by Category ID
            posts.Sort((a, b) =>
            {
                int aCat = a?.Categories?.Length > 0 ? a.Categories.Min() : int.MaxValue;
                int bCat = b?.Categories?.Length > 0 ? b.Categories.Min() : int.MaxValue;
                return aCat.CompareTo(bCat);
            });

            List<Carousel>? FeaturedRecipes = new List<Carousel>();

            foreach (var post in posts)
            {
                FeaturedRecipes.Add(new Carousel(post.FeaturedImageUrl, post.Link));
            }

            RecipesCarousel.ItemsSource = FeaturedRecipes;
        }
        private async void OnAboutClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//AboutPage");
        }

        private async void OnBreakfastClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//BreakfastPage");
        }

        private async void OnLunchClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LunchPage");
        }

        private async void OnDinnerClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//DinnerPage");
        }

        private async void OnSnacksClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//SnacksPage");
        }

        private async void OnImageTapped(object sender, EventArgs e)
        {
            if (sender is Image image && image.BindingContext is Carousel SelectedRecipe)
            {
                if (SelectedRecipe != null)
                {
                    string? url = SelectedRecipe.Link;

                    await Shell.Current.GoToAsync($"RecipeDetailsPage?url={url}");
                }
            }
        }
    }
}
