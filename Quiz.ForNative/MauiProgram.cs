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
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Quiz.ForNative.Services.Interface.Auth;
using Quiz.ForNative.Domain;

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

            LoadConfiguration(builder);

            SetUpService(builder.Services, builder.Configuration);

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        private static void LoadConfiguration(MauiAppBuilder builder)
        {
            var a = Assembly.GetExecutingAssembly();
#if DEBUG
            using var stream = a.GetManifestResourceStream("Quiz.ForNative.appsettings.Development.json");
#else
            using var stream = a.GetManifestResourceStream("Quiz.ForNative.appsettings.Local.json");
#endif

            var config = new ConfigurationBuilder()
                        .AddJsonStream(stream)
                        .Build();


            builder.Configuration.AddConfiguration(config);
        }

        private static void SetUpService(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddRepositories()
                .AddMappers()
                .AddServices()
                .AddViewModels()
                .AddHttpUtilities(configuration)
                .AddPages();
        }

        private static IServiceCollection AddPages(this IServiceCollection services)
        {
            services.AddSingleton<MainPage>()
                .AddSingleton<RegisterView>()
                .AddSingleton<LoginView>();
            return services;
        }

        private static IServiceCollection AddMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(RegisterMapper).Assembly);
            return services;
        }

        private static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddScoped<IRegisterViewModel, RegisterViewModel>()
                .AddScoped<IConnectViewModel, LoginViewModel>();
            return services;
        }

        private static IServiceCollection AddHttpUtilities(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient("QuizHttpClient", (httpClient) =>
                {
                    httpClient.BaseAddress = new Uri(configuration["ApiUrl"] ?? throw new Exception("You must define an api url"));
                })
                .AddHttpMessageHandler<ConflictHandler>()
                .AddHttpMessageHandler<UnAuthorizedAccessHandler>()
                .AddHttpMessageHandler<BadRequestHandler>();
            services.AddTransient<ConflictHandler>()
                .AddTransient<UnAuthorizedAccessHandler>()
                .AddTransient<BadRequestHandler>();
            services.AddTransient<IQuizHttpClient>(sp => {
                HttpClient httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("QuizHttpClient");
                return new QuizHttpClient(httpClient);
            });
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<Services.Interface.RegisterService<RegisterDto>, RegisterService>()
                .AddSingleton<IConnectService<User>, ConnectService>();
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IAuthRepository<UserDto>, AuthApiRepository>();
            return services;
        }
    }
}
