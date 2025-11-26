namespace MoonShot_Bogucki.Pages;

public partial class ApolloPage : ContentPage
{
    public ApolloPage()
    {
        InitializeComponent();
        // Hide the back button from the UI
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior
        {
            IsVisible = false
        });
    }

    protected override bool OnBackButtonPressed()
    {
        // Return true to cancel the default back behavior
        return true;
    }

    private async void OnMissionClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is string destination)
        {
            switch (destination)
            {
                // Testing Missions
                case "Apollo1":
                    await Shell.Current.GoToAsync("//TestingPage/Apollo1Page");
                    break;
                case "Apollo4":
                    await Shell.Current.GoToAsync("//TestingPage/Apollo4Page");
                    break;
                case "Apollo5":
                    await Shell.Current.GoToAsync("//TestingPage/Apollo5Page");
                    break;
                case "Apollo6":
                    await Shell.Current.GoToAsync("//TestingPage/Apollo6Page");
                    break;
                case "Apollo7":
                    await Shell.Current.GoToAsync("//TestingPage/Apollo7Page");
                    break;
                case "Apollo8":
                    await Shell.Current.GoToAsync("//TestingPage/Apollo8Page");
                    break;
                case "Apollo9":
                    await Shell.Current.GoToAsync("//TestingPage/Apollo9Page");
                    break;
                case "Apollo10":
                    await Shell.Current.GoToAsync("//TestingPage/Apollo10Page");
                    break;

                // Landing Missions
                case "Apollo11":
                    await Shell.Current.GoToAsync("//LandingPage/Apollo11Page");
                    break;
                case "Apollo12":
                    await Shell.Current.GoToAsync("//LandingPage/Apollo12Page");
                    break;
                case "Apollo13":
                    await Shell.Current.GoToAsync("//LandingPage/Apollo13Page");
                    break;
                case "Apollo14":
                    await Shell.Current.GoToAsync("//LandingPage/Apollo14Page");
                    break;

                // Advanced Missions
                case "Apollo15":
                    await Shell.Current.GoToAsync("//AdvancedPage/Apollo15Page");
                    break;
                case "Apollo16":
                    await Shell.Current.GoToAsync("//AdvancedPage/Apollo16Page");
                    break;
                case "Apollo17":
                    await Shell.Current.GoToAsync("//AdvancedPage/Apollo17Page");
                    break;

                default:
                    // Optional: handle unknown command
                    break;
            }
        }
    }
}