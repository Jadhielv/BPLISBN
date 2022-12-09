using BPLISBN.Extensions;
using BPLISBN.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BPLISBN.Services
{
    public class FileConstructorService : IFileConstructorService
    {
        private readonly ILogger<FileConstructorService> _logger;

        public FileConstructorService(ILogger<FileConstructorService> logger)
        {
            _logger = logger;
        }

        public void Run(List<Book> items, string fileName)
        {
            _logger.LogInformation("Start creating the file");

            var _outputFolder = string.Empty;
            var builder = new ConfigurationBuilder()
                 .AddJsonFile($"appsettings.json", true, true);
            var config = builder.Build();
            _outputFolder = config["AppSettings:OutputFolder"];

            using (StreamWriter writer = new($"{_outputFolder}\\{fileName}.csv"))
            {
                writer.WriteLine($"Row Number,Data Retrieval Type,ISBN,Title,Subtitle,Author Name(s),Number of Pages,Publish Date");
                int rowNum = 0;

                foreach (var item in items)
                {
                    writer.WriteLine($"{++rowNum}," +
                                     $"{item.DataRetrievalType.GetEnumDescription()}," +
                                     $"{item.ISBN}," +
                                     $"{item.title}," +
                                     $"{item.subtitle}," +
                                     $"{string.Join(", ", item.by_statement)}," +
                                     $"{item.number_of_pages}," +
                                     $"{item.publish_date}");
                }
            }

            _logger.LogInformation("End file creation");
        }
    }
}
