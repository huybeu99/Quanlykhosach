using QuanLyKhoSach.Models;

namespace QuanLyKhoSach.Interface.Repository;

public interface IAuthorRepository
{
    Task<IEnumerable<Author>> GetAllAuthorAsync();
    Task<Author> GetAllAuthorByIDAsync(int id);
    Task<Author> AddAuthorAsync(Author author);
    Task<Author> UpdateAuthorAsync(Author author);
    Task DeleteAuthorAsync(int id);
}