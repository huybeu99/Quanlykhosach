using AutoMapper;
using QuanLyKhoSach.DTO;
using QuanLyKhoSach.Interface.Repository;
using QuanLyKhoSach.Interface.Service;
using QuanLyKhoSach.Models;
using System.Text.Json;

namespace QuanLyKhoSach.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        public async Task<BookDTO> AddBookAsync(BookDTO bookDTO)
        {
            if (bookDTO == null)
            {
                throw new ArgumentNullException(nameof(bookDTO));
            }
            var book = _mapper.Map<Book>(bookDTO);
            var addedBook = await _bookRepository.AddBookAsync(book);
            return _mapper.Map<BookDTO>(addedBook);
        }
        public async Task<BookDTO> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            return _mapper.Map<BookDTO>(book);
        }
        public async Task<IEnumerable<BookDTO>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public async Task<BookDTO> UpdateBookAsync(BookDTO bookdto)
        {
            var jsonBookDto = JsonSerializer.Serialize(bookdto);
            Console.WriteLine($"Full BookDTO: {jsonBookDto}");
            // Validate dữ liệu đầu vào
            if (bookdto == null)
            {
                throw new ArgumentNullException(nameof(bookdto));
            }

            // Kiểm tra các trường bắt buộc
            if (string.IsNullOrWhiteSpace(bookdto.Book_Name))
            {
                throw new ArgumentException("Tên sách không được để trống");
            }

            if (bookdto.Publisher_ID <= 0)
            {
                throw new ArgumentException("Mã nhà xuất bản không hợp lệ");
            }

            if (bookdto.WareHouse_ID <= 0)
            {
                throw new ArgumentException("Mã kho sách không hợp lệ");
            }

            var book = _mapper.Map<Book>(bookdto);
            var updatedBook = await _bookRepository.UpdateBookAsync(book);
            return _mapper.Map<BookDTO>(updatedBook);
        }
        public async Task DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteBookAsync(id);
        }

        public async Task<IEnumerable<BookDTO>> SearchBooksAsync(string name = null)
        {
            var books = await _bookRepository.SearchBooksAsync(name);
            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public async Task<BookAuthorDTO> AddAuthorToBookAsync(BookAuthorDTO bookauthordto)
        {
            var bookauthor = _mapper.Map<BookAuthor>(bookauthordto);
            var addedbookauthor = await _bookRepository.AddAuthorToBookAsync(bookauthor);
            return _mapper.Map<BookAuthorDTO>(bookauthor);
        }
    }
}