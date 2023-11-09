namespace ApiIntegration.Cli.Models
{
    public record RestaurantSearchRequest
    {
        public RestaurantSearchRequest(string outCode)
        {
            OutCode = outCode;
        }
        public string OutCode { get; private set; }
    }
}
