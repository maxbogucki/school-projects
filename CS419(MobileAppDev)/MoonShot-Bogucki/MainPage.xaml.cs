namespace MoonShot_Bogucki
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnMissionClicked(object sender, EventArgs e)
        {
            // Navigate to the ApolloPage within the MainPage tab
            await Shell.Current.GoToAsync("//MainPage/ApolloPage");
        }
    }
}