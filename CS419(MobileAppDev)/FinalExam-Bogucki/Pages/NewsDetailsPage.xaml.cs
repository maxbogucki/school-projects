namespace FinalExam_Bogucki.Pages;

public partial class NewsDetailsPage : ContentPage, IQueryAttributable
{
	public required string URL { get; set; }
    public NewsDetailsPage()
	{
		InitializeComponent();
        Shell.SetTitleColor(this, Colors.White);
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        if(!Preferences.ContainsKey("DestinationCity") && !Preferences.ContainsKey("DestinationCountry"))
        {
            await Shell.Current.GoToAsync("..");
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        // Extract the URL from the query parameters
        if (query.TryGetValue("url", out var urlObj) && urlObj is string url)
        {
            URL = url;
        }

        BindingContext = this;
    }

    private void OnWebViewNavigating(object sender, WebNavigatingEventArgs e)
    {
        // Show the ActivityIndicator when the page starts loading
        BusyIndicator.IsRunning = true;
        BusyIndicator.IsVisible = true;

        NewsWebView.IsVisible = false;
    }

    private void OnWebViewNavigated(object sender, WebNavigatedEventArgs e)
    {
        // Hide the ActivityIndicator when the page finishes loading
        BusyIndicator.IsRunning = false;
        BusyIndicator.IsVisible = false;

        NewsWebView.IsVisible = true;
    }
}