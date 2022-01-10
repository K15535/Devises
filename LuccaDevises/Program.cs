using LuccaDevises.Service;
using Microsoft.Extensions.DependencyInjection;

namespace LuccaDevises
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // TODO : Check args

            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            App? app = services.AddSingleton<App, App>().BuildServiceProvider().GetService<App>();

            try
            {
                // TODO : if app null throw exception
                
                app.Run(args[0]);
            }
            catch (Exception ex)
            {

            }
            // TODO : Catch and filter all specific exceptions
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IPathService, PathService>()
                .AddSingleton<IExchangeRateService, ExchangeRateService>()
                .AddSingleton<IParsingService, ParsingService>();
        }
    }
}
