using QuanLyKhoSach.DTO;

namespace QuanLyKhoSach.Interface.Service;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDTO>> GetCategoryAsync();
    Task<CategoryDTO> GetCategoryByIDAsync(int id);
    Task<CategoryDTO> AddCategoryAsync(CategoryDTO categorydto);
    Task<CategoryDTO> UpdateCategoryAsync(CategoryDTO categorydto);
    Task DeleteCategoryAsync(int id);
}