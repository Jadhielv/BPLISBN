using BPLISBN.Models;

namespace BPLISBN.Services
{
    public interface IAPIConsumerService
    {
        Task<Book?> CallAPI(string item, string fileName);
        void Run(string[] items, string fileName);
    }
}