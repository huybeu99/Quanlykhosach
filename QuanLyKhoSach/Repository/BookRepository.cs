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


        //public async Task<Book> UpdateBookAsync(Book book)
        //{
        //    var ExitingBook = await _context.Book.FirstOrDefaultAsync(b => b.Book_ID == book.Book_ID);
        //    if (ExitingBook == null)
        //    {
        //        throw new NotFoundException("Không tìm thấy gì");
        //    }
        //    ExitingBook.Book_ID = book.Book_ID;
        //    ExitingBook.Book_Name = book.Book_Name;
        //    ExitingBook.Book_Year = book.Book_Year;
        //    ExitingBook.Book_Description = book.Book_Description;
        //    ExitingBook.Book_Quantity = book.Book_Quantity;
        //    ExitingBook.Publisher_ID = book.Publisher_ID;
        //    ExitingBook.WareHouse_ID = book.WareHouse_ID;
        //    ExitingBook.Images = book.Images ?? ExitingBook.Images;
        //    // Cập nhật mối quan hệ nhiều-nhiều BookAuthor
        //    if (book.BookAuthor != null)
        //    {
        //        // Xóa các mối quan hệ cũ
        //        _context.Book_Author.RemoveRange(ExitingBook.BookAuthor);

        //        // Thêm các mối quan hệ mới
        //        ExitingBook.BookAuthor = book.BookAuthor;
        //    }

        //    // Cập nhật mối quan hệ nhiều-nhiều BookCategory
        //    if (book.BookCategory != null)
        //    {
        //        // Xóa các mối quan hệ cũ
        //        _context.Book_Category.RemoveRange(ExitingBook.BookCategory);

        //        // Thêm các mối quan hệ mới
        //        ExitingBook.BookCategory = book.BookCategory;
        //    }

        //    await _context.SaveChangesAsync();
        //    return ExitingBook;
        //}
        public async Task<Book> UpdateBookAsync(Book book)
        {
            try
            {  // Kiểm tra sự tồn tại của Publisher
                var publisherExists = await _context.Publisher
                    .AnyAsync(p => p.Publisher_ID == book.Publisher_ID);

                if (!publisherExists)
                {
                    throw new ArgumentException($"Nhà xuất bản với ID {book.Publisher_ID} không tồn tại");
                }

                // Kiểm tra sự tồn tại của Warehouse
                var warehouseExists = await _context.WareHouse
                    .AnyAsync(w => w.WareHouse_ID == book.WareHouse_ID);

                if (!warehouseExists)
                {
                    throw new ArgumentException($"Kho sách với ID {book.WareHouse_ID} không tồn tại");
                }

                var existingBook = await _context.Book
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
