using Microsoft.EntityFrameworkCore;
using QuanLyKhoSach.Controllers;
using QuanLyKhoSach.Interface.Repository;
using QuanLyKhoSach.Models;
using System.Net;

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
            return await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.WareHouse)
                .Include(b => b.BookAuthor).ThenInclude(ba => ba.Author)
                .Include(b => b.BookCategory).ThenInclude(bc => bc.Category)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _context.Book
                .Include(b => b.Publisher)
                .Include(b => b.WareHouse)
                .Include(b => b.BookAuthor)
                .ThenInclude(ba => ba.Author)
                .Include(b => b.BookCategory)
                .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(b => b.Book_ID == id);
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            _context.Book.Add(book);
            await _context.SaveChangesAsync();
            return book;
            //Add với SaveChange là phương thức của DbSet
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await _context.Book.FirstOrDefaultAsync(b => b.Book_ID == id);
            var relatedBookAuthors = _context.Book_Author.Where(ba => ba.Book_ID == id);
            var relatedBookCategory = _context.Book_Category.Where(bc => bc.Book_ID == id);

            _context.Book_Author.RemoveRange(relatedBookAuthors);
            _context.Book_Category.RemoveRange(relatedBookCategory);

            _context.Book.Remove(book);
            await _context.SaveChangesAsync();
        }
        public async Task<Book> UpdateBookAsync(Book book)
        {
            try
            {  
                var publisherExists = await _context.Publisher
                    .AnyAsync(p => p.Publisher_ID == book.Publisher_ID);

                if (!publisherExists)
                {
                    throw new ArgumentException($"Nhà xuất bản với ID {book.Publisher_ID} không tồn tại");
                }

               
                var warehouseExists = await _context.WareHouse
                    .AnyAsync(w => w.WareHouse_ID == book.WareHouse_ID);

                if (!warehouseExists)
                {
                    throw new ArgumentException($"Kho sách với ID {book.WareHouse_ID} không tồn tại");
                }

                var existingBook = await _context.Book
                    .Include(b => b.BookAuthor) // Include Author relationship
                    .Include(b => b.BookCategory) // Include Category relationship if needed
                    .FirstOrDefaultAsync(b => b.Book_ID == book.Book_ID);

                if (existingBook == null)
                {
                    throw new NotFoundException("Không tìm thấy sách");
                }

                // Cập nhật các thuộc tính
                existingBook.Book_Name = book.Book_Name;
                existingBook.Book_Year = book.Book_Year;
                existingBook.Book_Description = book.Book_Description;
                existingBook.Book_Quantity = book.Book_Quantity;
                existingBook.Publisher_ID = book.Publisher_ID;
                existingBook.WareHouse_ID = book.WareHouse_ID;
                
                _context.Book_Author.RemoveRange(existingBook.BookAuthor);
                if (book.BookAuthor != null && book.BookAuthor.Any())
                {
                    foreach (var bookauthor in book.BookAuthor)
                    {
                        Console.WriteLine($"Author ID received: {bookauthor.Author_ID}");

                        var authorExitts=await _context.Author.AnyAsync(a => a.Author_ID == bookauthor.Author_ID);
                        if (!authorExitts)
                        {
                            throw new ArgumentException($"Tác giả với ID {bookauthor.Author_ID} không tồn tại");
                        }
                        var newBookAuthor = new BookAuthor
                        {
                            Book_ID = existingBook.Book_ID,
                            Author_ID = bookauthor.Author_ID
                        };
                
                        _context.Book_Author.Add(newBookAuthor);
                    }
                }
                
                _context.Book_Category.RemoveRange(existingBook.BookCategory);
                if (book.BookCategory != null && book.BookCategory.Any())
                {
                    foreach (var bookcategory in book.BookCategory)
                    {
                        var categoryExitts=await _context.Category.AnyAsync(a => a.Category_ID == bookcategory.Category_ID);
                        if (!categoryExitts)
                        {
                            throw new ArgumentException($"Tác giả với ID {bookcategory.Category_ID} không tồn tại");
                        }
                        var newBookCategory = new BookCategory
                        {
                            Book_ID = existingBook.Book_ID,
                            Category_ID = bookcategory.Category_ID,
                        };
                
                        _context.Book_Category.Add(newBookCategory);
                    }
                }

                await _context.SaveChangesAsync();
                return existingBook;
            }
            catch (Exception ex)
            {
                // Ghi chi tiết lỗi và InnerException để dễ dàng theo dõi nguyên nhân
                Console.WriteLine($"Error updating book: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                throw new Exception("An error occurred while saving the entity changes. See the inner exception for details.", ex);
            }
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string name = null)
        {
            IQueryable<Book> query = _context.Book.Include(b => b.Publisher)
                .Include(b => b.BookAuthor).ThenInclude(ba => ba.Author)
                .Include(b => b.BookCategory).ThenInclude(bc => bc.Category);
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(b => b.Book_Name.Contains(name));
            }
            return await query.ToListAsync();
        }

        public async Task<BookAuthor> AddAuthorToBookAsync(BookAuthor bookauthor)
        {
            _context.Book_Author.Add(bookauthor);
            await _context.SaveChangesAsync();
            return bookauthor;
        }
    }
}
