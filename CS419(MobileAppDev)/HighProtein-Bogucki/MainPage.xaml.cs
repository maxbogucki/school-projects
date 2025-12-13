using HighProtein.Services;
using System.Threading.Tasks;

namespace HighProtein_Bogucki
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            GetFeaturedPosts();
        }

        private async void GetFeaturedPosts()
        {
            var wp = new WordPressClient("https://wp.elmhurst.edu/highprotein");
            var posts = await wp.GetPostsByTagAsync(6, 1, 100);

            // Sort by Category ID
            posts.Sort((a, b) =>
            {
                int aCat = a?.Categories?.Length > 0 ? a.Categories.Min() : int.MaxValue;
                int bCat = b?.Categories?.Length > 0 ? a.Categories.Min() : int.MaxValue;
                return aCat.CompareTo(bCat);
            });

            foreach (var post in posts)
            {
                await DisplayAlert("Recipe Title", post?.Title?.Rendered, "Ok");
            }
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
    }
}
