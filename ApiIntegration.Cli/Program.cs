using ApiIntegration.Cli.Api;
using ApiIntegration.Cli.Output;
using ApiIntegration.Cli.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Refit;
using System.Text.Json;

namespace ApiIntegration.Cli
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IConfigurationRoot configuration = BuildConfiguration();
            ServiceProvider serviceProvide = BuildServiceProvider(configuration);

            var app = serviceProvide.GetRequiredService<RestaurantSearchApplication>();
            await app.RunAsync(args);
        }

        private static ServiceProvider BuildServiceProvider(IConfigurationRoot configuration)
        {
            var services = new ServiceCollection();
            ConfigureServices(configuration, services);

            var serviceProvide = services.BuildServiceProvider();
            return serviceProvide;
        }

        private static void ConfigureServices(IConfigurationRoot configuration, ServiceCollection services)
        {
            services.AddSingleton<RestaurantSearchApplication>();
            services.AddSingleton<IConsoleWriter, ConsoleWriter>();
            services.AddSingleton<IRestaurantService, RestaurantService>();
            services.AddValidatorsFromAssemblyContaining<Program>();

            services.AddRefitClient<IResturatnApi>()
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
                }))
                .ConfigureHttpClient(httpClient =>
                {
                    httpClient.BaseAddress = new Uri(configuration["Restaurant:BaseUrl"]);
                });
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}