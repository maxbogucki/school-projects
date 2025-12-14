namespace FinalExam_Bogucki
{
    public partial class App : Application
    {
        // Destinations Dictionary (City -> Country)
        public static readonly Dictionary<string, string> DestinationsDictionary = new()
    {
        { "Banff", "Canada" },
        { "Bangkok", "Thailand" },
        { "Barcelona", "Spain" },
        { "Buenos Aires", "Argentina" },
        { "Cape Town", "South Africa" },
        { "Cusco", "Peru" },
        { "Dubai", "United Arab Emirates" },
        { "Hanoi", "Viet Nam" },
        { "Istanbul", "Turkey" },
        { "Kyoto", "Japan" },
        { "Lisbon", "Portugal" },
        { "Marrakech", "Morocco" },
        { "New York", "United States of America" },
        { "Paris", "France" },
        { "Prague", "Czechia" },
        { "Queenstown", "New Zealand" },
        { "Reykjavik", "Iceland" },
        { "Rio de Janeiro", "Brazil" },
        { "Rome", "Italy" },
        { "Sydney", "Australia" }
    };

        // Country Codes Dictionary (Country -> Code)
        public static readonly Dictionary<string, string> CountryCodesDictionary = new()
    {
        { "Canada", "ca" },
        { "Thailand", "th" },
        { "Spain", "es" },
        { "Argentina", "ar" },
        { "South Africa", "za" },
        { "Peru", "pe" },
        { "United Arab Emirates", "ae" },
        { "Viet Nam", "vn" },
        { "Turkey", "tr" },
        { "Japan", "jp" },
        { "Portugal", "pt" },
        { "Morocco", "ma" },
        { "United States of America", "us" },
        { "France", "fr" },
        { "Czechia", "cz" },
        { "New Zealand", "nz" },
        { "Iceland", "is" },
        { "Brazil", "br" },
        { "Italy", "it" },
        { "Australia", "au" }
    };

        // Latitude Dictionary (City -> Latitude)
        public static readonly Dictionary<string, double> LatitudeDictionary = new()
    {
        { "Banff", 51.1777781 },
        { "Bangkok", 13.7524938 },
        { "Barcelona", 41.3828939 },
        { "Buenos Aires", -34.6075682 },
        { "Cape Town", -33.9288301 },
        { "Cusco", -13.5170887 },
        { "Dubai", 25.2653471 },
        { "Hanoi", 21.0294498 },
        { "Istanbul", 41.0091982 },
        { "Kyoto", 35.021041 },
        { "Lisbon", 38.7077507 },
        { "Marrakech", 31.6258257 },
        { "New York", 40.7127281 },
        { "Paris", 48.8588897 },
        { "Prague", 50.0874654 },
        { "Queenstown", -45.0321923 },
        { "Reykjavik", 64.145981 },
        { "Rio de Janeiro", -22.9110137 },
        { "Rome", 41.8933203 },
        { "Sydney", -33.8698439 }
    };

        // Longitude Dictionary (City -> Longitude)
        public static readonly Dictionary<string, double> LongitudeDictionary = new()
    {
        { "Banff", -115.5682504 },
        { "Bangkok", 100.4935089 },
        { "Barcelona", 2.1774322 },
        { "Buenos Aires", -58.4370894 },
        { "Cape Town", 18.4172197 },
        { "Cusco", -71.9785356 },
        { "Dubai", 55.2924914 },
        { "Hanoi", 105.8544441 },
        { "Istanbul", 28.9662187 },
        { "Kyoto", 135.7556075 },
        { "Lisbon", -9.1365919 },
        { "Marrakech", -7.9891608 },
        { "New York", -74.0060152 },
        { "Paris", 2.3200410 },
        { "Prague", 14.4212535 },
        { "Queenstown", 168.6626 },
        { "Reykjavik", -21.9422367 },
        { "Rio de Janeiro", -43.2093727 },
        { "Rome", 12.4829321 },
        { "Sydney", 151.2082848 }
    };
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}