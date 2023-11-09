using ApiIntegration.Cli.Api.Responses;
using ApiIntegration.Cli.Models;
using OneOf;

namespace ApiIntegration.Cli.Services
{
    public interface IRestaurantService
    {
        Task<OneOf<RestaurantSerachResult, RestaurantSearchError>> SearchByOutCodeAsync(RestaurantSearchRequest request);
    }
}
