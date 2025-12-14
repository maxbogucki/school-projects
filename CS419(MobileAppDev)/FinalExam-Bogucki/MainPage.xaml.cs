using static FinalExam_Bogucki.App;
namespace FinalExam_Bogucki
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (Preferences.ContainsKey("DestinationCity") && Preferences.ContainsKey("DestinationCountry"))
            {
                string DestinationCity = Preferences.Get("DestinationCity", string.Empty);
                string DestinationCountry = Preferences.Get("DestinationCountry", string.Empty);

                ShowDestination(DestinationCity, DestinationCountry);
            }
            else
            {
                HideDestination();
                LoadDestination();
            }
        }

        public void LoadDestination()
        {
            var destinations = new List<string>(DestinationsDictionary.Keys);
            DestinationPicker.ItemsSource = destinations;
        }

        private void OnDestinationSelected(object sender, EventArgs e)
        {
            var selectedKey = DestinationPicker.SelectedItem as string;

            if (selectedKey != null && DestinationsDictionary.TryGetValue(selectedKey, out string? selectedValue))
            {
                Preferences.Set("DestinationCity", selectedKey);
                Preferences.Set("DestinationCountry", selectedValue);

                ShowDestination(selectedKey, selectedValue);
            }
        }

        private void ShowDestination(string City, string Country)
        {
            DestinationLabel.Text = $"{City}\n{Country}";
            DestinationLabel.IsVisible = true;
            DestinationBoxTop.IsVisible = true;
            DestinationBoxBottom.IsVisible = true;
            DestinationPicker.IsVisible = false;

            TravelGuideButton.IsVisible = true;
            NewsButton.IsVisible = true;
            WeatherButton.IsVisible = true;
        }

        private void HideDestination()
        {
            DestinationLabel.Text = string.Empty;
            DestinationLabel.IsVisible = false;
            DestinationBoxTop.IsVisible = false;
            DestinationBoxBottom.IsVisible = false;
            DestinationPicker.IsVisible = true;

            DestinationPicker.SelectedIndex = -1;

            TravelGuideButton.IsVisible = false;
            NewsButton.IsVisible = false;
            WeatherButton.IsVisible = false;
        }

        private async void OnTravelGuideButtonClicked(object sender, EventArgs e)
        {
            string DestinationCity = Preferences.Get("DestinationCity", string.Empty);
            DestinationCity = DestinationCity.Replace(" ", "-").ToLower();

            string url = $"https://wp.elmhurst.edu/travel/guide/{DestinationCity}/";

            await Shell.Current.GoToAsync($"TravelGuidePage?url={url}");
        }

        private async void OnNewsButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("NewsPage");
        }
        private async void OnWeatherButtonClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("WeatherPage");
        }
    }
}
