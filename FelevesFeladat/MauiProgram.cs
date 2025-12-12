using FelevesFeladat.Service;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;

namespace FelevesFeladat
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiMaps()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
            builder.Services.AddSingleton<DataBase>();
            builder.Services.AddTransient<Pages.ReviewsPage>();
            builder.Services.AddTransient<Pages.MapPage>();
            builder.Services.AddTransient<Pages.ReviewEditorPage>();
            builder.Services.AddSingleton<Pages.MyReviewPage>();
            builder.Services.AddSingleton<ViewModels.MyReviewPageViewModel>(); 
            builder.Services.AddTransient<ViewModels.ReviewEditorPageViewModel>();
            builder.Services.AddTransient<ViewModels.MapPageViewModel>();
            builder.Services.AddTransient<ViewModels.ReviewsPageViewModel>();
#endif

            return builder.Build();
        }
    }
}
