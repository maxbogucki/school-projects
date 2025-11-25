namespace MoonShot_Bogucki.Pages.Advanced;

public partial class AdvancedPage : ContentPage
{
    public AdvancedPage()
    {
        InitializeComponent();
    }

    private async void OnMissionClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is string destination)
        {
            switch (destination)
            {
                case "Apollo15":
                    await Shell.Current.GoToAsync("Apollo15Page");
                    break;
                case "Apollo16":
                    await Shell.Current.GoToAsync("Apollo16Page");
                    break;
                case "Apollo17":
                    await Shell.Current.GoToAsync("Apollo17Page");
                    break;
                default:
                    break;
            }
        }
    }
}