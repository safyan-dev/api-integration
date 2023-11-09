using ApiIntegration.Cli.Api;
using ApiIntegration.Cli.Mapping;
using ApiIntegration.Cli.Models;
using FluentValidation;
using OneOf;

namespace ApiIntegration.Cli.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IResturatnApi _resturatnApi;
        private readonly IValidator<RestaurantSearchRequest> _validator;

        public RestaurantService(IResturatnApi resturatnApi, IValidator<RestaurantSearchRequest> validator)
        {
            _resturatnApi = resturatnApi;
            _validator = validator;
        }

        public async Task<OneOf<RestaurantSerachResult, RestaurantSearchError>> SearchByOutCodeAsync(RestaurantSearchRequest request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new RestaurantSearchError(errorMessages);
            }

            var response = await _resturatnApi.SearchByOutCodeAsync(request.OutCode);
            return new RestaurantSerachResult
            {
                RestaurantResult = response.Restaurants.Select(x => x.ToRestaurantResult()).ToList()
            };
        }
    }
}
