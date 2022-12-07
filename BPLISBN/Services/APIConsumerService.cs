using BPLISBN.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BPLISBN.Services
{
    internal class APIConsumerService : IAPIConsumerService
    {
        private readonly ILogger<APIConsumerService> _logger;
        private readonly string? _apiEndpoint;

        public APIConsumerService(ILogger<APIConsumerService> logger, IOptions<AppSettings> settings)
        {
            _logger = logger;
            _apiEndpoint = settings.Value.APIEndpoint;
        }

        public async Task<Book?> CallAPI(string item, string fileName)
        {
            _logger.LogInformation("Call to the API");

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync($"{_apiEndpoint}/{item}.json");

                if (response.IsSuccessStatusCode && response.Content.Headers.ContentLength > 0)
                {
                    var element = JsonConvert.DeserializeObject<Book?>(await response.Content.ReadAsStringAsync());

                    if (element != null)
                    {
                        element.DataRetrievalType = DataRetrievalType.Server;
                        element.ISBN = item;
                        return element;
                    }
                }
                else
                {
                    _logger.LogError($"Problem retrieving data from: {item}");
                }
            }

            _logger.LogInformation("End call the API");
            return null;
        }

        public async void Run(string[] items, string fileName)
        {
            _logger.LogInformation("Call to the API");
            var books = new List<Book>();

            using (var httpClient = new HttpClient())
            {
                foreach (var item in items)
                {
                    var response = await httpClient.GetAsync($"{_apiEndpoint}/{item}.json");
                    if (response.IsSuccessStatusCode && response.Content.Headers.ContentLength > 0)
                    {
                        var element = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());

                        if (element != null)
                        {
                            element.DataRetrievalType = DataRetrievalType.Server;
                            element.ISBN = item;
                            books.Add(element);
                        }
                    }
                    else
                    {
                        _logger.LogError($"Problem retrieving data from: {item}");
                    }
                }
            }

            if (books.Any())
                BookContext.Instance.Books.AddRange(books);
            _logger.LogInformation("End call the API");
        }
    }
}
