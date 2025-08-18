using MVC_Session2.Models;

namespace MVC_Session2
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task SaveChangesAsync();
        Task AddAsync(Book book);
        Task UpdateAsync(int id, Book book);
        Task DeleteAsync(int id);


    }
}
