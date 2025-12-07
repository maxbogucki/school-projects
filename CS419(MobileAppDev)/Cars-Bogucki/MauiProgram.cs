using Cars_Bogucki.Services;
using Cars_Bogucki.ViewModels;
using Cars_Bogucki.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace Cars_Bogucki;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        // Services
        builder.Services.AddSingleton<DatabaseService>();
        builder.Services.AddSingleton<CarRepository>();

        // ViewModels
        builder.Services.AddTransient<CarListViewModel>();
        builder.Services.AddTransient<CarDetailViewModel>();

        // Views
        builder.Services.AddTransient<CarListPage>();
        builder.Services.AddTransient<CarDetailPage>();

        var app = builder.Build();

        // Seed the database on startup
        var repo = app.Services.GetRequiredService<CarRepository>();
        Task.Run(async () => await repo.SeedAsync()).Wait();

        return app;
    }
}