using Microsoft.EntityFrameworkCore;
using QuanLyKhoSach.Interface.Repository;
using QuanLyKhoSach.Models;

namespace QuanLyKhoSach.Repository;

public class AuthorRepository : IAuthorRepository
{
    private readonly BookDbContext _context;

    public AuthorRepository(BookDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Author>> GetAllAuthorAsync()
    {
          return _context.Author
              .Include(b => b.BookAuthors)
              .ThenInclude(ba => ba.Book)
              .AsSplitQuery()
              .AsNoTracking()
              .ToList();
    }

    public async Task<Author> GetAllAuthorByIDAsync(int id)
    {
        return await _context.Author
            .Include(b => b.BookAuthors )
            .ThenInclude(ba => ba.Book)
            .FirstOrDefaultAsync(b => b.Author_ID == id);
    }

    public async Task<Author> AddAuthorAsync(Author author)
    {
        _context.Author.Add(author);
        await _context.SaveChangesAsync();
        return author;
    }

    public Task<Author> UpdateAuthorAsync(Author author)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAuthorAsync(int id)
    {
        throw new NotImplementedException();
    }
}