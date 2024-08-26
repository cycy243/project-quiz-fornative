using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using Quiz.Dtos;
using Quiz.ForNative.Middleware.Http;
using Quiz.ForNative.Repository;
using Quiz.ForNative.Repository.Interfaces;
using Quiz.ForNative.Mappers;
using Quiz.ViewModels.Interface;
using Quiz.ViewModels;
using Quiz.ForNative.Views.Auth;
using Quiz.ForNative.Services;
using Quiz.ForNative.Repository.Http;

namespace Quiz.ForNative
{
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

            SetUpService(builder.Services);

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void SetUpService(IServiceCollection services)
        {
            services
                .AddRepositories()
                .AddMappers()
                .AddServices()
                .AddViewModels()
                .AddHttpUtilities()
                .AddPages();
        }

        private static IServiceCollection AddPages(this IServiceCollection services)
        {
            services.AddSingleton<MainPage>()
                .AddSingleton<RegisterView>();
            return services;
        }

        private static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(RegisterMapper).Assembly);
            return services;
        }

        private static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddScoped<IRegisterViewModel, RegisterViewModel>();
            return services;
        }

        private static IServiceCollection AddHttpUtilities(this IServiceCollection services)
        {
            services.AddHttpClient("QuizHttpClient", (httpClient) =>
                {
                    httpClient.BaseAddress = new Uri("http://192.168.4.37:3000");
                })
                .AddHttpMessageHandler<ConflictHandler>()
                .AddHttpMessageHandler<UnAuthorizedAccessHandler>();
            services.AddTransient<ConflictHandler>()
                .AddTransient<UnAuthorizedAccessHandler>();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<Services.Interface.RegisterService<RegisterDto>, RegisterService>();
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IAuthRepository<UserDto>>(pr =>
            {
                HttpClient httpClient = pr.GetRequiredService<IHttpClientFactory>().CreateClient("QuizHttpClient");
                return new AuthApiRepository(new QuizHttpClient(httpClient));
            });
            return services;
        }
    }
}
