using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using QuanLyKhoSach.DTO;
using QuanLyKhoSach.Interface.Repository;
using QuanLyKhoSach.Interface.Service;
using QuanLyKhoSach.Models;
using QuanLyKhoSach.Repository;

namespace QuanLyKhoSach.Services
{
    public class BookService: IBookService
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
            if (book == null)
            {
                throw new DirectoryNotFoundException("Không tìm thấy");
            }
            return _mapper.Map<BookDTO>(book);
        }
        public async Task<IEnumerable<BookDTO>> GetAllBooksAsync()
        {
            var books=await _bookRepository.GetAllBooksAsync();
            return _mapper.Map<IEnumerable<BookDTO>>(books);        
        }

        public async Task<BookDTO> UpdateBookAsync(BookDTO bookdto)
        {
            var book = _mapper.Map<Book>(bookdto);
            await _bookRepository.UpdateBookAsync(book);
            return _mapper.Map<BookDTO>(book);
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
    }
}