using BPLISBN.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BPLISBN
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddScoped<Startup, Startup>()
                .AddScoped<IReadContentService, ReadContentService>()
                .AddScoped<IAPIConsumerService, APIConsumerService>()
                .AddScoped<IFileConstructorService, FileConstructorService>()
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
            logger.LogDebug("Starting application");
            var initiate = serviceProvider.GetService<Startup>();
            initiate.Start();
            logger.LogDebug("All done!");
        }
    }
}