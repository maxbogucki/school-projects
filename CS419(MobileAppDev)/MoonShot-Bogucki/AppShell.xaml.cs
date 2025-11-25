namespace MoonShot_Bogucki
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Register main page
            Routing.RegisterRoute("ApolloPage", typeof(Pages.ApolloPage));

            // Register Testing missions
            Routing.RegisterRoute("Apollo1Page", typeof(Pages.Testing.Apollo1Page));
            Routing.RegisterRoute("Apollo4Page", typeof(Pages.Testing.Apollo4Page));
            Routing.RegisterRoute("Apollo5Page", typeof(Pages.Testing.Apollo5Page));
            Routing.RegisterRoute("Apollo6Page", typeof(Pages.Testing.Apollo6Page));
            Routing.RegisterRoute("Apollo7Page", typeof(Pages.Testing.Apollo7Page));
            Routing.RegisterRoute("Apollo8Page", typeof(Pages.Testing.Apollo8Page));
            Routing.RegisterRoute("Apollo9Page", typeof(Pages.Testing.Apollo9Page));
            Routing.RegisterRoute("Apollo10Page", typeof(Pages.Testing.Apollo10Page));

            // Register Landing missions
            Routing.RegisterRoute("Apollo11Page", typeof(Pages.Landing.Apollo11Page));
            Routing.RegisterRoute("Apollo12Page", typeof(Pages.Landing.Apollo12Page));
            Routing.RegisterRoute("Apollo13Page", typeof(Pages.Landing.Apollo13Page));
            Routing.RegisterRoute("Apollo14Page", typeof(Pages.Landing.Apollo14Page));

            // Register Advanced missions
            Routing.RegisterRoute("Apollo15Page", typeof(Pages.Advanced.Apollo15Page));
            Routing.RegisterRoute("Apollo16Page", typeof(Pages.Advanced.Apollo16Page));
            Routing.RegisterRoute("Apollo17Page", typeof(Pages.Advanced.Apollo17Page));
        }
    }
}