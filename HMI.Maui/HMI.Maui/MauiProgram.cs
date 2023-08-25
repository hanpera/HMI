using CommunityToolkit.Maui;
using Syncfusion.Maui.Core.Hosting;
using HMI.Maui.Services;
using HMI.Maui.ViewModels;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui.Storage;

namespace HMI.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
            .ConfigureSyncfusionCore()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                ;

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddHttpClient<EventsService>(options =>
            {
                options.BaseAddress = new Uri("http://localhost:5073/");
            });

            builder.Services.AddScoped<EventsViewModel>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton(FileSaver.Default);
            return builder.Build();
        }
    }
}
