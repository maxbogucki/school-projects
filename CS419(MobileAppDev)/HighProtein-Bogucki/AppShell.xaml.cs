using HighProtein_Bogucki.Pages;

namespace HighProtein_Bogucki
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("RecipeDetailsPage", typeof(RecipeDetailsPage));
        }
    }
}
