using Microsoft.AspNetCore.Mvc;
using QuanLyKhoSach.Interface.Service;
using QuanLyKhoSach.Models;

namespace QuanLyKhoSach.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthorController : BaseController
{
    private readonly IAuthorService _authorService;
    public AuthorController(IAuthorService authorService)
    {
        _authorService = authorService;
    }

    [HttpGet("allauthor")]
    public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthor()
    {
        try
        {
            var author = await _authorService.GetAllAuthorAsync();
            return Ok(author);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}