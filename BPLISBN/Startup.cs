using BPLISBN.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BPLISBN
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;
        private readonly string? _inputFolder;
        private FileSystemWatcher _folderWatcher;
        private readonly IServiceProvider _services;

        public Startup(ILogger<Startup> logger, IOptions<AppSettings> settings, FileSystemWatcher folderWatcher, IServiceProvider services)
        {
            _logger = logger;
            _inputFolder = settings.Value.InputFolder;
            _folderWatcher = folderWatcher;
            _services = services;
        }

        public void Start()
        {
            _logger.LogInformation("Starting");
            if (!Directory.Exists(_inputFolder))
            {
                _logger.LogWarning($"Please make sure the input folder [{_inputFolder}] exists, then restart the application");
            }

            _logger.LogInformation($"Binding events from input folder: {_inputFolder}");
            _folderWatcher = new FileSystemWatcher(_inputFolder, "*.TXT")
            {
                NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite | NotifyFilters.FileName |
                                  NotifyFilters.DirectoryName
            };
            _folderWatcher.Created += Input_OnChanged;
            _folderWatcher.EnableRaisingEvents = true;
        }

        protected void Input_OnChanged(object source, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                _logger.LogInformation($"Inbound change event triggered by [{e.FullPath}]");

                using (var scope = _services.CreateScope())
                {
                    var readContent = scope.ServiceProvider.GetRequiredService<IReadContentService>();
                    readContent.Run(e.FullPath, e.Name);
                }
                _logger.LogInformation("Done with the inbound change event");
            }
        }
    }
}
