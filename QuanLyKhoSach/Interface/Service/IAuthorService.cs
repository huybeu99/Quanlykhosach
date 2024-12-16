using QuanLyKhoSach.DTO;

namespace QuanLyKhoSach.Interface.Service;

public interface IAuthorService
{
    Task<IEnumerable<AuthorDTO>> GetAllAuthorAsync();
    Task<AuthorDTO> GetAllAuthorByIDAsync(int id);
    Task<AuthorDTO> AddAuthorAsync(AuthorDTO authordto);
    Task<AuthorDTO> UpdateAuthorAsync(AuthorDTO authordto);
    Task DeleteAuthorAsync(int id);
}