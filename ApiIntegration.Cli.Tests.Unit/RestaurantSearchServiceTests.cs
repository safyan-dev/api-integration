using ApiIntegration.Cli.Api;
using ApiIntegration.Cli.Api.Responses;
using ApiIntegration.Cli.Mapping;
using ApiIntegration.Cli.Models;
using ApiIntegration.Cli.Services;
using ApiIntegration.Cli.Validators;
using AutoFixture;
using FluentAssertions;
using FluentValidation;
using NSubstitute;

namespace ApiIntegration.Cli.Tests.Unit
{
    public class RestaurantSearchServiceTests
    {
        private readonly RestaurantService _sut;
        private readonly IResturatnApi _resturatnApi = Substitute.For<IResturatnApi>();
        private readonly IValidator<RestaurantSearchRequest> _validator = new RestaurantSerachRequestValidator();
        private readonly IFixture _fixture = new Fixture();

        public RestaurantSearchServiceTests()
        {
            _sut = new RestaurantService(_resturatnApi, _validator);
        }

        [Fact]
        public async Task SearchByCodeAsync_ShouldReturnResult_WhenOutCodeIsValid()
        {
            //Arange
            const string code = "E2";
            var request = new RestaurantSearchRequest(code);

            var apiResponse = _fixture.Create<RestaurantSearchResponse>();
            _resturatnApi.SearchByOutCodeAsync(code).Returns(apiResponse);

            var expectedResult = new RestaurantSerachResult
            {
                RestaurantResult = apiResponse.Restaurants.Select(x => x.ToRestaurantResult()).ToList()
            };

            //Act
            var result = await _sut.SearchByOutCodeAsync(request);

            //Assert

            result.AsT0.Should().BeEquivalentTo(expectedResult, options =>
                options.ComparingByMembers<RestaurantSerachResult>()
                .ComparingByMembers<RestaurantResult>());

        }

        [Fact]
        public async Task SearchByCodeAsync_ShouldReturnError_WhenOutCodeIsInValid()
        {
            //Arange
            const string code = "1AA";
            var request = new RestaurantSearchRequest(code);

            var errors = new[] { "Provide valid code." };
            var expectedResult = new RestaurantSearchError(errors);

            //Act
            var result = await _sut.SearchByOutCodeAsync(request);

            //Assert

            result.AsT1.Should().BeEquivalentTo(expectedResult, options =>
                options.ComparingByMembers<RestaurantSearchError>());

        }
    }
}