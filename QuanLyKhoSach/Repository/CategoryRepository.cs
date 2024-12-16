using Microsoft.EntityFrameworkCore;
using QuanLyKhoSach.Interface.Repository;
using QuanLyKhoSach.Models;

namespace QuanLyKhoSach.Repository;

public class CategoryRepository:ICategoryRepository
{
    private readonly BookDbContext _context;

    public CategoryRepository(BookDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Category>> GetCategoryAsync()
    {
        return await _context.Category.ToListAsync();
    }

    public async Task<Category> GetCategoryByIDAsync(int id)
    {
        return await _context.Category.FirstOrDefaultAsync(c => c.Category_ID == id);
    }

    public Task<Category> AddCategoryAsync(Category category)
    {
        throw new NotImplementedException();
    }

    public Task<Category> UpdateCategoryAsync(Category category)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCategoryAsync(int id)
    {
        throw new NotImplementedException();
    }
}