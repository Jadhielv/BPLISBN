using BPLISBN.Models;
using Microsoft.Extensions.Logging;

namespace BPLISBN.Services
{
    public class ReadContentService : IReadContentService
    {
        private readonly ILogger<ReadContentService> _logger;
        private readonly IAPIConsumerService _apiConsumerService;
        private readonly IFileConstructorService _fileConstructorService;

        public ReadContentService(ILogger<ReadContentService> logger, IAPIConsumerService apiConsumerService, IFileConstructorService fileConstructorService)
        {
            _logger = logger;
            _apiConsumerService = apiConsumerService;
            _fileConstructorService = fileConstructorService;
        }

        public void Run(string inputFile, string fileName)
        {
            _logger.LogInformation($"Start to read file content: {fileName}");

            string[] lines = File.ReadAllLines(inputFile);
            List<string> rows = new();

            foreach (string line in lines)
            {
                rows.AddRange(line.Split(','));
            }

            _logger.LogInformation("End of reading the file content");

            var existingBooks = BookContext.Instance.Books;
            var books = new List<Book>();

            foreach (string row in rows)
            {
                var book = new Book();
                var exists = existingBooks.Any(x => x.ISBN == row);

                if (exists)
                {
                    book = existingBooks.FirstOrDefault(x => x.ISBN == row);
                    if (book != null)
                        book.DataRetrievalType = DataRetrievalType.Cache;
                }
                else
                {
                    book = _apiConsumerService.CallAPI(row, fileName).Result;
                    if (book != null)
                        BookContext.Instance.Books.Add(book);
                }

                if (book != null)
                {
                    books.Add(book);
                }
            }

            _fileConstructorService.Run(books, fileName);
        }
    }
}
