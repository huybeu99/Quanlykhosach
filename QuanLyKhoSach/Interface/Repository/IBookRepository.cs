using QuanLyKhoSach.Models;

namespace QuanLyKhoSach.Interface.Repository
{
    public interface IBookRepository  
    {
        Task<Book> GetBookByIdAsync(int id);
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> AddBookAsync(Book book);
        Task<Book> UpdateBookAsync(Book book);
        Task DeleteBookAsync(int id);
        Task<BookAuthor> AddAuthorToBookAsync(BookAuthor bookauthor);
        Task<IEnumerable<Book>> SearchBooksAsync(string title);
    }

}

