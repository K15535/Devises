using LuccaDevises.Exception;
using LuccaDevises.Service;
using Microsoft.Extensions.DependencyInjection;

namespace LuccaDevises
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CheckArgs(args);

            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            App? app = services.AddSingleton<App, App>().BuildServiceProvider().GetService<App>();

            try
            {
                if (app == null)
                    throw new NullServiceException();

                app.Run(args[0]);
            }
            catch (ShortestPathNotFoundException ex)
            {
                Console.Error.WriteLine("Calculation not possible: no shortest path found");
            }
            catch (NullServiceException ex)
            {
                Console.Error.WriteLine("Error while setting up dependency injection");
            }
            catch (IncorrectExchangeRateDataFormatException ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                Console.Error.WriteLine($"{args[0]} not found");
            }
           catch (FileMissingLinesException ex)
            {
                Console.Error.WriteLine("File is missing lines");
            }
            catch (System.Exception ex)
            {
                Console.Error.WriteLine("Unprocessed exception:");
                Console.Error.WriteLine(ex.ToString());
            }
            // TODO : Catch and filter all specific exceptions
        }

        /// <summary>
        /// Checks args length. Prints USAGE and quit in case of error.
        /// </summary>
        /// <param name="args">Program arguments to process</param>
        private static void CheckArgs(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine($"USAGE: {System.AppDomain.CurrentDomain.FriendlyName}.exe PATH_TO_FILE");
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Configure the services to inject in the program.
        /// </summary>
        /// <param name="services">The service collection</param>
        private static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IPathService, PathService>()
                .AddSingleton<IExchangeRateService, ExchangeRateService>()
                .AddSingleton<IParsingService, ParsingService>();
        }
    }
}
