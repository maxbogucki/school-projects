namespace HighProtein_Bogucki
{
    public partial class App : Application
    {
        public enum RECIPES : int
        {
            BREAKFAST = 2,
            LUNCH = 3,
            DINNER = 4,
            SNACKS = 5,
            FEATURED = 6
        }
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