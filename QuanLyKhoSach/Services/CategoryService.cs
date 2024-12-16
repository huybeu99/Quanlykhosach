using AutoMapper;
using QuanLyKhoSach.DTO;
using QuanLyKhoSach.Interface.Repository;
using QuanLyKhoSach.Interface.Service;

namespace QuanLyKhoSach.Services;

public class CategoryService:ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategoryAsync()
    {
        var books= await _categoryRepository.GetCategoryAsync();
        return _mapper.Map<IEnumerable<CategoryDTO>>(books);
    }

    public async Task<CategoryDTO> GetCategoryByIDAsync(int id)
    {
        var book= await _categoryRepository.GetCategoryByIDAsync(id);
        return _mapper.Map<CategoryDTO>(book);
    }

    public Task<CategoryDTO> AddCategoryAsync(CategoryDTO categorydto)
    {
        throw new NotImplementedException();
    }

    public Task<CategoryDTO> UpdateCategoryAsync(CategoryDTO categorydto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCategoryAsync(int id)
    {
        throw new NotImplementedException();
    }
}