using AutoMapper;
using QuanLyKhoSach.DTO;
using QuanLyKhoSach.Interface.Repository;
using QuanLyKhoSach.Interface.Service;
using QuanLyKhoSach.Models;

namespace QuanLyKhoSach.Services
{
    public class PublisherService:IPublisherService
    {
        private readonly IPublisherRepository _publisherrepository;
        private readonly IMapper _mapper;
        public PublisherService(IPublisherRepository publisherrepository, IMapper mapper)
        {
            _publisherrepository = publisherrepository;
            _mapper = mapper;
        }

        public async Task<PublisherDTO> AddPublisherAsync(PublisherDTO publisherDTO)
        {
            var publisher = _mapper.Map<Publisher>(publisherDTO);
            var addedwarehouse = await _publisherrepository.AddPublisherAsync(publisher);
            return _mapper.Map<PublisherDTO>(publisher);
        }

        public async Task DeletePublisherAsync(int id)
        {
            await _publisherrepository.DeletePublisherAsync(id);
        }

        public async Task<IEnumerable<PublisherDTO>> GetPublisherAsync()
        {
            var publishers = await _publisherrepository.GetPublisherAsync();
            return _mapper.Map<IEnumerable<PublisherDTO>>(publishers);
        }

        public Task<PublisherDTO> GetPublisherByID(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PublisherDTO> GetPublisherByIDAsync(int id)
        {
            var publisher = await _publisherrepository.GetPublisherByIDAsync(id);
            return _mapper.Map<PublisherDTO>(publisher);
        }

        public async Task<PublisherDTO> UpdatePublisherAsync(PublisherDTO publisherdto)
        {
            var publisher = _mapper.Map<Publisher>(publisherdto);
            await _publisherrepository.UpdatePublisherAsync(publisher);
            return _mapper.Map<PublisherDTO>(publisher);
        }
    }
}
