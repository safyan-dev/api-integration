using ApiIntegration.Cli.Api.Responses;
using ApiIntegration.Cli.Models;

namespace ApiIntegration.Cli.Mapping
{
    public static class ContractToModelMapping
    {
        public static RestaurantResult ToRestaurantResult(this RestaurantResponse response)
        {
            return new()
            {
                Name = response.Name,
                Rating = response.RatingStars,
                CuisineTypes = response.CuisineTypes.Select(c => c.Name).ToList()
            };
        }
    }
}
