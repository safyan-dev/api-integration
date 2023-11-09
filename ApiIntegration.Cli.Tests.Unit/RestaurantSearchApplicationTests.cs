using ApiIntegration.Cli.Api.Responses;
using ApiIntegration.Cli.Models;
using ApiIntegration.Cli.Services;
using AutoFixture;
using NSubstitute;
using ApiIntegration.Cli.Output;
using OneOf;
using System.Text.Json;

namespace ApiIntegration.Cli.Tests.Unit
{
    public class RestaurantSearchApplicationTests
    {

        private readonly RestaurantSearchApplication _sut;
        private readonly IConsoleWriter _consoleWriter = Substitute.For<IConsoleWriter>();
        private readonly IRestaurantService _restaurantService = Substitute.For<IRestaurantService>();
        private readonly IFixture _fixture = new Fixture();

        public RestaurantSearchApplicationTests()
        {
            _sut = new RestaurantSearchApplication(_consoleWriter, _restaurantService);
        }

        [Fact]
        public async Task RunAsync_ShouldReturnRestaurants_WhenOutCodeIsValid()
        {
            //Arange
            const string code = "E2";
            var args = new[] { "--o", code };

            var restaurantResult = _fixture.Create<RestaurantSerachResult>();
            OneOf<RestaurantSerachResult, RestaurantSearchError> result = restaurantResult;

            var searchResult = new RestaurantSearchRequest(code);
            _restaurantService.SearchByOutCodeAsync(searchResult).Returns(result);

            var expectedSerializedText = JsonSerializer.Serialize(restaurantResult, new JsonSerializerOptions
            {
                WriteIndented = true,
            });

            //Act
            await _sut.RunAsync(args);

            //Assert
            _consoleWriter.Received(1).Write(Arg.Is(expectedSerializedText));
        }

        [Fact]
        public async Task RunAsync_ShouldReturnErrorMessage_WhenOutCodeIsInValid()
        {
            //Arange
            const string code = "E2";
            var args = new[] { "--o", code };
            const string invalidOutcodeError = "Provide valid code.";

            var errorResult = new RestaurantSearchError(new[] { invalidOutcodeError });
            OneOf<RestaurantSerachResult, RestaurantSearchError> result = errorResult;

            var searchResult = new RestaurantSearchRequest(code);
            _restaurantService.SearchByOutCodeAsync(searchResult).Returns(result);

            //Act
            await _sut.RunAsync(args);

            //Assert
            _consoleWriter.Received(1).Write(Arg.Is(invalidOutcodeError));
        }
    }
}
