using QuanLyKhoSach.DTO;
using QuanLyKhoSach.Models;

namespace QuanLyKhoSach.Interface.Service
{
    public interface IBookService
    {
        Task<IEnumerable<BookDTO>> GetAllBooksAsync();
        Task<BookDTO> GetBookByIdAsync(int id);
        Task<BookDTO> AddBookAsync(BookDTO bookDto);
        Task<BookDTO> UpdateBookAsync(BookDTO bookDto);
        Task DeleteBookAsync(int id);
        Task<BookAuthorDTO> AddAuthorToBookAsync(BookAuthorDTO bookauthordto);
        Task<IEnumerable<BookDTO>> SearchBooksAsync(string title);

    }
}
