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
    public class BookController : ControllerBase
    {

        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("allbooks")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAllBooks()
        {
            try
            {
                var books = await _bookService.GetAllBooksAsync();
                return Ok(books);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

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
            Console.WriteLine($"Received BookDTO - Publisher_ID: {bookDTO.Publisher_ID} - WareHouse_ID:{bookDTO.WareHouse_ID}");
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

        //// Thêm tác giả cho sách
        //[HttpPost("{bookId}/authors")]
        //public async Task<IActionResult> AddAuthorToBook(int bookId, int authorId)
        //{
        //    await _bookService.AddAuthorToBook(bookId, authorId);
        //    return Ok();
        //}
    }
        // Custom Exception
        public class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message) { }
        }
    }