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
          return _context.Author.ToList();
    }

    public async Task<Author> GetAllAuthorByIDAsync(int id)
    {
        return await _context.Author.FirstOrDefaultAsync(b => b.Author_ID == id);
    }

    public Task<Author> AddAuthorAsync(Author author)
    {
        throw new NotImplementedException();
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