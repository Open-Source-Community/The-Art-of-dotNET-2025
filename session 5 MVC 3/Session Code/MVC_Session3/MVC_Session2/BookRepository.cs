using Microsoft.EntityFrameworkCore;
using MVC_Session2.Data;
using MVC_Session2.Models;

namespace MVC_Session2
{
    public class BookRepository : IBookRepository
    {
        AppDbContext context;
        //Don't Forget to Register AppDbContext in Main => Go to Program.cs
        public BookRepository(AppDbContext context) {
            this.context = context;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await context.Books.Include(b=>b.Category).ToListAsync();
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await context.Books.Include(b=>b.Category).FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }

        public async Task AddAsync(Book book)
        {
            context.Books.Add(book);
            await SaveChangesAsync();
        }
        public async Task UpdateAsync(int id,Book book)
        {
            var oldBook = await GetByIdAsync(id);
            oldBook.Title = book.Title;
            oldBook.Description = book.Description;
            oldBook.CategoryId = book.CategoryId;
            await SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            Book? book = await GetByIdAsync(id);
            if (book != null)
            {
                context.Remove(book);
                await SaveChangesAsync();
            }

        }
    }
}
