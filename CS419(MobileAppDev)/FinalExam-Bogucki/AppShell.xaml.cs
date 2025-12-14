using FinalExam_Bogucki.Pages;

namespace FinalExam_Bogucki
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("TravelGuidePage", typeof(TravelGuidePage));
            Routing.RegisterRoute("NewsPage", typeof(NewsPage));
            Routing.RegisterRoute("NewsDetailsPage", typeof(NewsDetailsPage));
            Routing.RegisterRoute("WeatherPage", typeof(WeatherPage));

        }
    }
}
