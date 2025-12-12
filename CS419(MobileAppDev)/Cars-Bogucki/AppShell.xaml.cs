namespace Cars_Bogucki;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(Views.CarDetailPage), typeof(Views.CarDetailPage));
    }
}