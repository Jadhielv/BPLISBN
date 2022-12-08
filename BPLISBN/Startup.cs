using BPLISBN.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BPLISBN
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;
        private readonly IServiceProvider _services;

        public Startup(ILogger<Startup> logger, IServiceProvider services)
        {
            _logger = logger;
            _services = services;
        }

        public void Start()
        {
            _logger.LogInformation("Starting");

            var _inputFolder = string.Empty;
            var builder = new ConfigurationBuilder()
                 .AddJsonFile($"appsettings.json", true, true);

            var config = builder.Build();
            _inputFolder = config["AppSettings:InputFolder"];

            if (!Directory.Exists(_inputFolder))
            {
                _logger.LogWarning($"Please make sure the input folder [{_inputFolder}] exists, then restart the application");
            }

            _logger.LogInformation($"Binding events from input folder: {_inputFolder}");

            var txtFiles = Directory.EnumerateFiles(_inputFolder, "*.txt");
            foreach (string currentFile in txtFiles)
            {
                using (var scope = _services.CreateScope())
                {
                    var readContent = scope.ServiceProvider.GetRequiredService<IReadContentService>();
                    var name = currentFile.Substring(_inputFolder.Length);
                    readContent.Run(currentFile, name.Substring(1, name.Length - 5));
                }
            }
        }
    }
}
