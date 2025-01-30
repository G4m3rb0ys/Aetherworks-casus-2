using Microsoft.Extensions.Logging;
using Aetherworks_casus_2.Data;
using Plugin.LocalNotification;
using ZXing.Net.Maui.Controls;
using Aetherworks_casus_2.MVVM.Views;
using Aetherworks_casus_2.MVVM.ViewModels;

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
                })
                .UseBarcodeReader();
#if DEBUG
    		builder.Logging.AddDebug();
#endif
            // Register services
            builder.Services.AddSingleton<LocalDbService>();

            // Register pages
            builder.Services.AddTransient<QRscanViewModel>();
            builder.Services.AddSingleton<QRscanPage>();

            return builder.Build();
        }
    }
}
