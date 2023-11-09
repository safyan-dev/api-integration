using System.Text.Json.Serialization;

namespace ApiIntegration.Cli.Api.Responses
{
    public record RestaurantSearchResponse
    {
        [JsonPropertyName("restaurants")]
        public IReadOnlyList<RestaurantResponse> Restaurants { get; set; }
    }
}
