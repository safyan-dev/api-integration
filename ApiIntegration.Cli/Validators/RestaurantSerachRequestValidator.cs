using ApiIntegration.Cli.Models;
using FluentValidation;

namespace ApiIntegration.Cli.Validators
{
    public class RestaurantSerachRequestValidator : AbstractValidator<RestaurantSearchRequest>
    {
        public RestaurantSerachRequestValidator()
        {
            RuleFor(request => request.OutCode)
                .Matches("^([A-Za-z][0-9]{1,2})$")
                .WithMessage("Provide valid code.");
        }
    }
}
