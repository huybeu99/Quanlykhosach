using AutoMapper;
using QuanLyKhoSach.DTO;
using QuanLyKhoSach.Interface.Repository;
using QuanLyKhoSach.Interface.Service;

namespace QuanLyKhoSach.Services;

public class AuthorService:IAuthorService
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;
    public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuthorDTO>> GetAllAuthorAsync()
    {
        var authors= await _authorRepository.GetAllAuthorAsync();
        return _mapper.Map<IEnumerable<AuthorDTO>>(authors);
    }

    public async Task<AuthorDTO> GetAllAuthorByIDAsync(int id)
    {
        var author = await _authorRepository.GetAllAuthorByIDAsync(id);
        return _mapper.Map<AuthorDTO>(author);
    }

    public Task<AuthorDTO> AddAuthorAsync(AuthorDTO authordto)
    {
        throw new NotImplementedException();
    }

    public Task<AuthorDTO> UpdateAuthorAsync(AuthorDTO authordto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAuthorAsync(int id)
    {
        throw new NotImplementedException();
    }
}