using Microsoft.EntityFrameworkCore;
using QuanLyKhoSach.Interface.Repository;
using QuanLyKhoSach.Models;

namespace QuanLyKhoSach.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _context;
        public BookRepository(BookDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _context.Books
                .Include(b => b.Publisher)
                .Include(b => b.WareHouse)
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Include(b => b.BookCategories).ThenInclude(bc => bc.Category)
                .ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Publisher)
                .Include(b => b.WareHouse)
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .Include(b => b.BookCategories)
                .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(b => b.Book_ID == id);
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            _context.Books.Add(book); 
            await _context.SaveChangesAsync();
            return book;
            //Add với SaveChange là phương thức của DbSet
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b=>b.Book_ID==id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }


        public async Task<Book> UpdateBookAsync(Book book)
        {
            var ExitingBook = await _context.Books.FirstOrDefaultAsync(b => b.Book_ID == book.Book_ID);
            if (ExitingBook == null)
            {
                throw new DirectoryNotFoundException("Không tìm thấy gì");
            }
            ExitingBook.Book_ID = book.Book_ID;
            ExitingBook.Book_Name = book.Book_Name;
            ExitingBook.Year = book.Year;
            ExitingBook.Book_Description = book.Book_Description;
            ExitingBook.Book_Quantity = book.Book_Quantity;
            ExitingBook.Publisher_ID= book.Publisher_ID;
            ExitingBook.WareHouse_ID = book.WareHouse_ID;
       
            await _context.SaveChangesAsync();
            return ExitingBook;
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string name = null)
        {
            IQueryable<Book> query = _context.Books.Include(b => b.Publisher)
                .Include(b => b.BookAuthors).ThenInclude(ba => ba.Author)
                .Include(b => b.BookCategories).ThenInclude(bc => bc.Category);
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(b => b.Book_Name.Contains(name));
            }
            return await query.ToListAsync();
        }

    }
}
