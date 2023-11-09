using ApiIntegration.Cli.Api.Responses;
using Refit;

namespace ApiIntegration.Cli.Api
{
    public interface IResturatnApi
    {
        [Get("/restaurants/bypostcode/{postCode}")]
        Task<RestaurantSearchResponse> SearchByOutCodeAsync(string postCode);
    }
}