using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using QuanLyKhoSach.DTO;
using QuanLyKhoSach.Interface.Service;
using QuanLyKhoSach.Services;

namespace QuanLyKhoSach.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : BaseController
    {

        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;
        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }
        [HttpGet("allbooks")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllBooks()
        {
            try
            {
                _logger.LogInformation("Attempting to retrieve all books");
                var books = await _bookService.GetAllBooksAsync();

                if (books == null || !books.Any())
                {
                    _logger.LogWarning("No books found");
                    return NoContent();
                }

                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving books");
                return StatusCode(500, "An error occurred while retrieving books");
            }
        }

        //[HttpGet("allbooks")]
        //public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllBooks()
        //{
        //    try
        //    {
        //        var books = await _bookService.GetAllBooksAsync();
        //        return Ok(books);
        //    }
        //    catch (NotFoundException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //}

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetBookByID(int id)
        {
            try
            {
                var book = await _bookService.GetBookByIdAsync(id);
                return Ok(book);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> CreateBook([FromBody]BookDTO bookDto)
        {
            try
            {
                var createdBook = await _bookService.AddBookAsync(bookDto);
                return CreatedAtAction(
                    nameof(GetBookByID),
                    new { id = createdBook.Book_ID },
                    createdBook
                );
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDTO>> UpdateBook([FromBody]BookDTO bookDTO,int id)
        {
            if (id != bookDTO.Book_ID)
            {
                return BadRequest(new { message = "ID không khớp" });
            }
            try
            {
                var updatebook = await _bookService.UpdateBookAsync(bookDTO);
                return Ok(updatebook);
            }
            catch (NotFoundException)
            {
                return NotFound(new { message = "Không tìm thấy sách" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi cập nhật sách", error = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookDTO>> DeleteBook(int id)
        {
            try
            {
                 await _bookService.DeleteBookAsync(id);
                return NoContent();
            }
            catch (DirectoryNotFoundException)
            {
                return NotFound(new { message = "Không tìm thấy sách" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi xóa sách", error = ex.Message });
            }
        }
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> SearchBooks([FromQuery] string name = null)
        {
            try
            {
                var books = await _bookService.SearchBooksAsync(name);
                return Ok(books);
            } 
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "An error occurred while processing your request");
            }
        }
    }
        // Custom Exception
        public class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message) { }
        }
    }