namespace FinalExam_Bogucki.Pages;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (Preferences.ContainsKey("DestinationCity") && Preferences.ContainsKey("DestinationCountry"))
        {
            ResetDestinationButton.BackgroundColor = Colors.Firebrick;
            ResetDestinationButton.TextColor = Colors.White;
            ResetDestinationButton.IsEnabled = true;
        }

        if (Preferences.ContainsKey("NewsCategory"))
        {
            ResetNewsCategoryButton.BackgroundColor = Colors.Firebrick;
            ResetNewsCategoryButton.TextColor = Colors.White;
            ResetNewsCategoryButton.IsEnabled = true;

            string NewsCategory = Preferences.Get("NewsCategory", string.Empty);

            switch (NewsCategory)
            {
                case "Business":
                    BusinessSwitch.ThumbColor = Colors.DarkOrange;
                    BusinessSwitch.IsToggled = true;
                    BusinessSwitch.IsEnabled = false;
                    break;
                case "Entertainment":
                    EntertainmentSwitch.ThumbColor = Colors.DarkOrange;
                    EntertainmentSwitch.IsToggled = true;
                    EntertainmentSwitch.IsEnabled = false;
                    break;
                case "General":
                    GeneralSwitch.ThumbColor = Colors.DarkOrange;
                    GeneralSwitch.IsToggled = true;
                    GeneralSwitch.IsEnabled = false;
                    break;
                case "Health":
                    HealthSwitch.ThumbColor = Colors.DarkOrange;
                    HealthSwitch.IsToggled = true;
                    HealthSwitch.IsEnabled = false;
                    break;
                case "Science":
                    ScienceSwitch.ThumbColor = Colors.DarkOrange;
                    ScienceSwitch.IsToggled = true;
                    ScienceSwitch.IsEnabled = false;
                    break;
                case "Sports":
                    SportsSwitch.ThumbColor = Colors.DarkOrange;
                    SportsSwitch.IsToggled = true;
                    SportsSwitch.IsEnabled = false;
                    break;
                case "Technology":
                    TechnologySwitch.ThumbColor = Colors.DarkOrange;
                    TechnologySwitch.IsToggled = true;
                    TechnologySwitch.IsEnabled = false;
                    break;
            }
        }
    }

    private async void OnResetDestinationButtonClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Confirm", "Are you sure you want to reset the travel destination?", "Yes", "No");

        if (confirm)
        {
            Preferences.Remove("DestinationCity");
            Preferences.Remove("DestinationCountry");

            ResetDestinationButton.BackgroundColor = Colors.LightGray;
            ResetDestinationButton.TextColor = Colors.Black;
            ResetDestinationButton.IsEnabled = false;
        }
    }

    private async void OnResetNewsCategoryButtonClicked(object sender, EventArgs e)
    {
        bool confirm = await DisplayAlert("Confirm", "Are you sure you want to reset your news category", "Yes", "No");
        if (confirm)
        {
            Preferences.Remove("NewsCategory");

            var categories = new List<Switch> { BusinessSwitch,
                EntertainmentSwitch,
                GeneralSwitch,
                HealthSwitch,
                ScienceSwitch,
                SportsSwitch,
                TechnologySwitch
            };

            foreach (var cat in categories)
            {
                cat.ThumbColor = Colors.Gray;
                cat.IsToggled = false;
                cat.IsEnabled = true;
            }

            ResetNewsCategoryButton.BackgroundColor = Colors.LightGray;
            ResetNewsCategoryButton.TextColor = Colors.Black;
            ResetNewsCategoryButton.IsEnabled = false;
        }
    }

    private void SaveNewsCategory(object sender, ToggledEventArgs e)
    {
        var category = sender as Switch;

        if (category == null)
        {
            return;
        }

        // if a switch is turned on, force the others off
        if (category.IsToggled)
        {
            var categories = new List<Switch> { BusinessSwitch,
                EntertainmentSwitch,
                GeneralSwitch,
                HealthSwitch,
                ScienceSwitch,
                SportsSwitch,
                TechnologySwitch
            };
            foreach (var cat in categories)
            {
                if (cat != category)
                {
                    cat.ThumbColor = Colors.Gray;
                    cat.IsToggled = false;
                    cat.IsEnabled = true;
                }
            }

            category.ThumbColor = Colors.DarkOrange;
            category.IsEnabled = false;

            ResetNewsCategoryButton.BackgroundColor = Colors.Firebrick;
            ResetNewsCategoryButton.TextColor = Colors.White;
            ResetNewsCategoryButton.IsEnabled = true;

            // save the preference for the active switch
            if (category == BusinessSwitch)
            {
                Preferences.Set("NewsCategory", "Business");
            }
            else if (category == EntertainmentSwitch)
            {
                Preferences.Set("NewsCategory", "Entertainment");
            }
            else if (category == GeneralSwitch)
            {
                Preferences.Set("NewsCategory", "General");
            }
            else if (category == HealthSwitch)
            {
                Preferences.Set("NewsCategory", "Health");
            }
            else if (category == ScienceSwitch)
            {
                Preferences.Set("NewsCategory", "Science");
            }
            else if (category == SportsSwitch)
            {
                Preferences.Set("NewsCategory", "Sports");
            }
            else if (category == TechnologySwitch)
            {
                Preferences.Set("NewsCategory", "Technology");
            }
        }
    }
}