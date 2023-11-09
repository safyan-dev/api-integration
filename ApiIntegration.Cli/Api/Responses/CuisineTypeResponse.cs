using System.Text.Json.Serialization;

namespace ApiIntegration.Cli.Api.Responses
{
    public class CuisineTypeResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}