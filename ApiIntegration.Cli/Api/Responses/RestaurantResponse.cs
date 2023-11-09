using System.Text.Json.Serialization;

namespace ApiIntegration.Cli.Api.Responses
{
    public class RestaurantResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("ratingStars")]
        public double RatingStars { get; set; }

        [JsonPropertyName("cuisineTypes")]
        public IReadOnlyList<CuisineTypeResponse> CuisineTypes { get; set; }
    }
}