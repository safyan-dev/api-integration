namespace ApiIntegration.Cli.Models
{
    public record RestaurantSearchError
    {
        public RestaurantSearchError(IReadOnlyList<string> errorMessages)
        {
            ErrorMessages = errorMessages;
        }
        public IReadOnlyList<string> ErrorMessages { get; init; }
    }
}
