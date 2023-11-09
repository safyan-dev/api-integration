namespace ApiIntegration.Cli.Models
{
    public class RestaurantResult
    {
        public string Name { get; init; }
        public double Rating { get; init; }
        public IReadOnlyList<string> CuisineTypes { get; init; }
    }
}
