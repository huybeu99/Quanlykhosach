using QuanLyKhoSach.Models;

namespace QuanLyKhoSach.Interface.Repository;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategoryAsync();
    Task<Category> GetCategoryByIDAsync(int id);
    Task<Category> AddCategoryAsync(Category category);
    Task<Category> UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(int id);
}