namespace ApiIntegration.Cli.Output
{
    public class ConsoleWriter : IConsoleWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
