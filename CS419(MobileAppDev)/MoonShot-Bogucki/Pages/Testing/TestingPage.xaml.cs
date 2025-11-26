namespace MoonShot_Bogucki.Pages.Testing;

public partial class TestingPage : ContentPage
{
    public TestingPage()
    {
        InitializeComponent();
    }

    private async void OnMissionClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.CommandParameter is string destination)
        {
            switch (destination)
            {
                case "Apollo1":
                    await Shell.Current.GoToAsync("Apollo1Page");
                    break;
                case "Apollo4":
                    await Shell.Current.GoToAsync("Apollo4Page");
                    break;
                case "Apollo5":
                    await Shell.Current.GoToAsync("Apollo5Page");
                    break;
                case "Apollo6":
                    await Shell.Current.GoToAsync("Apollo6Page");
                    break;
                case "Apollo7":
                    await Shell.Current.GoToAsync("Apollo7Page");
                    break;
                case "Apollo8":
                    await Shell.Current.GoToAsync("Apollo8Page");
                    break;
                case "Apollo9":
                    await Shell.Current.GoToAsync("Apollo9Page");
                    break;
                case "Apollo10":
                    await Shell.Current.GoToAsync("Apollo10Page");
                    break;
                default:
                    break;
            }
        }
    }
}