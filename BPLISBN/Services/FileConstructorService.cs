using BPLISBN.Extensions;
using BPLISBN.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BPLISBN.Services
{
    public class FileConstructorService : IFileConstructorService
    {
        private readonly ILogger<FileConstructorService> _logger;
        private readonly string? _outputFolder;

        public FileConstructorService(ILogger<FileConstructorService> logger, IOptions<AppSettings> settings)
        {
            _logger = logger;
            _outputFolder = settings.Value.OutputFolder;
        }

        public void Run(List<Book> items, string fileName)
        {
            _logger.LogInformation("Start creating the file");

            using (StreamWriter writer = new($"{_outputFolder}\\{fileName}.csv"))
            {
                writer.WriteLine($"Row Number,Data Retrieval Type,ISBN,Title,Subtitle,Author Name(s),Number of Pages,Publish Date");
                int rowNum = 0;

                foreach (var item in items)
                {
                    writer.WriteLine($"{++rowNum},{item.DataRetrievalType.GetEnumDescription()},{item.ISBN},{item.title},{item.subtitle},{string.Join("; ", item.authors?.Select(u => u.key))},{item.number_of_pages},{item.publish_date}");
                }
            }

            _logger.LogInformation("End file creation");
        }
    }
}
