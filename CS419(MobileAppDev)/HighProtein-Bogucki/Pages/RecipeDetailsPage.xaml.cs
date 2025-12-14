
namespace HighProtein_Bogucki.Pages
{
    public partial class RecipeDetailsPage : ContentPage, IQueryAttributable
    {
        public string? URL { get; set; }

        public RecipeDetailsPage()
        {
            InitializeComponent();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("url", out var urlObj) && urlObj is string url)
            {
                URL = url;
            }
            BindingContext = this;
        }

        public void OnNavigating(object sender, WebNavigatingEventArgs e)
        {
            BusyIndicator.IsRunning = true;
            BusyIndicator.IsVisible = true;
            RecipeWebView.IsVisible = false;
        }

        public void OnNavigated(object sender, WebNavigatedEventArgs e)
        {
            BusyIndicator.IsRunning = false;
            BusyIndicator.IsVisible = false;
            RecipeWebView.IsVisible = true;
        }

        private async void OnShareClicked(object sender, EventArgs e)
        {
            await DisplayAlert("ToDo", "Share feature", "Ok");
        }

        private async void OnFavoriteClicked(object sender, EventArgs e)
        {
            await DisplayAlert("ToDo", "Favorite feature", "Ok");
        }

        private async void OnHomeClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}