using CommandLine;

namespace ApiIntegration.Cli
{
    public class RestaurantSearchApplicationOption
    {
        [Option('o',"outcode", Required = true, HelpText = "Provide the outcode to perform the search.")]
        public string OutCode { get; set; }
    }
}
