namespace ApiIntegration.Cli.Models
{
    public record RestaurantSerachResult
    {
        public IReadOnlyList<RestaurantResult> RestaurantResult { get; init; }
    }
}
