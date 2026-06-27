using Microsoft.Extensions.Logging;
using Shuntghada.Services;


namespace Shuntghada
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            //  تسجيل الـ Database Service كـ Singleton ليكون متاحاً في كل التطبيق
            builder.Services.AddSingleton<LocalDatabaseService>();

            return builder.Build();
        }
    }
}
