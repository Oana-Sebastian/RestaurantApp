using System.Configuration;
using System.Data;
using System.Windows;
using RestaurantApp.Models;
using RestaurantApp.Views;
using Microsoft.Extensions.Hosting;
namespace RestaurantApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost AppHost { get; private set; }

        public static User CurrentUser { get; set; } // Your global user object

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Register EF Core with your connection string
                    services.AddDbContext<RestaurantContext>(options =>
                        options.UseSqlServer("Server=DESKTOP-2F7PF5E;Database=RestaurantAppDb;User Id=sa;Password=yourStrong(!)Password;"));

                    // Register services
                    services.AddScoped<IUserService, UserService>();
                    services.AddSingleton<INavigationService, ViewModelNavigationService>();

                    // Register viewmodels
                    services.AddTransient<LoginViewModel>();

                    // Register views
                    services.AddTransient<LoginView>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();

            // Open the main window
            var loginView = AppHost.Services.GetRequiredService<LoginView>();
            loginView.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost.StopAsync();
            base.OnExit(e);
        }
    }

}
