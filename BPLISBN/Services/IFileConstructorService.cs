using BPLISBN.Models;

namespace BPLISBN.Services
{
    public interface IFileConstructorService
    {
        void Run(List<Book> items, string fileName);
    }
}