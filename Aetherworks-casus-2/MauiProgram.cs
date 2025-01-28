using Microsoft.Extensions.Logging;
using Aetherworks_casus_2.Data;
using Plugin.LocalNotification;

namespace Aetherworks_casus_2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseLocalNotification()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            // Register services
            builder.Services.AddSingleton<LocalDbService>();


            return builder.Build();
        }
    }
}
