using Microsoft.AspNetCore.Mvc;
using QuanLyKhoSach.DTO;
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
            var authors = await _authorService.GetAllAuthorAsync();
            return Ok(authors);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDTO>> GetAuthorByID(int id)
    {
        try
        {
            var author = await _authorService.GetAllAuthorByIDAsync(id);
            return Ok(author);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<AuthorDTO>> CreateBook([FromBody] AuthorDTO authordto)
    {
        try
        {
            var createdauthor=await _authorService.AddAuthorAsync(authordto);
            return CreatedAtAction(
                nameof(GetAuthorByID),
                new { id = createdauthor.Author_ID}, createdauthor
            );
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}