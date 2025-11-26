namespace MoonShot_Bogucki.Pages.Landing;

public partial class LandingPage : ContentPage
{
    public LandingPage()
    {
        InitializeComponent();
    }

    private async void OnMissionClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is string destination)
        {
            switch (destination)
            {
                case "Apollo11":
                    await Shell.Current.GoToAsync("Apollo11Page");
                    break;
                case "Apollo12":
                    await Shell.Current.GoToAsync("Apollo12Page");
                    break;
                case "Apollo13":
                    await Shell.Current.GoToAsync("Apollo13Page");
                    break;
                case "Apollo14":
                    await Shell.Current.GoToAsync("Apollo14Page");
                    break;
                default:
                    break;
            }
        }
    }
}