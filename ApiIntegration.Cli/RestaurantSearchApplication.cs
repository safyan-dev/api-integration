using ApiIntegration.Cli.Models;
using ApiIntegration.Cli.Output;
using ApiIntegration.Cli.Services;
using CommandLine;
using OneOf;
using System.Text.Json;

namespace ApiIntegration.Cli
{
    public class RestaurantSearchApplication
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly IRestaurantService _restaurantService;
        public RestaurantSearchApplication(IConsoleWriter consoleWriter, IRestaurantService restaurantService)
        {
            _consoleWriter = consoleWriter;
            _restaurantService = restaurantService;
        }

        public async Task RunAsync(string[] args)
        {
            await Parser.Default
                .ParseArguments<RestaurantSearchApplicationOption>(args)
                .WithParsedAsync(async option =>
                {
                    var searchRequest = new RestaurantSearchRequest(option.OutCode);
                    var result = await _restaurantService.SearchByOutCodeAsync(searchRequest);
                    HandleSearchResult(option, result);
                });
        }

        private void HandleSearchResult(RestaurantSearchApplicationOption option, OneOf<RestaurantSerachResult, RestaurantSearchError> result)
        {
            result.Switch(searchResult =>
            {
                var formattedTextResult = JsonSerializer.Serialize(result, new JsonSerializerOptions
                {
                    WriteIndented = true,
                });
                _consoleWriter.Write($"The outcode was {option.OutCode}");
            },
            error =>
            {
                var formattedErrors = string.Join(",", error);
                _consoleWriter.Write(formattedErrors);
            });
        }
    }
}
