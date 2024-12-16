using Microsoft.AspNetCore.Mvc;
using QuanLyKhoSach.Interface.Service;
using QuanLyKhoSach.Models;

namespace QuanLyKhoSach.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CategoryController : BaseController
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("allcategory")]
    public async Task<ActionResult<IEnumerable<Category>>> GetAllCategory()
    {
        try
        {
            var category = await _categoryService.GetCategoryAsync();
            return Ok(category);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}