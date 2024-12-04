using AutoMapper;
using QuanLyKhoSach.DTO;
using QuanLyKhoSach.Interface.Repository;
using QuanLyKhoSach.Interface.Service;
using QuanLyKhoSach.Models;

namespace QuanLyKhoSach.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IMapper _mapper;
        public StoreService(IStoreRepository storeRepository, IMapper mapper)
        {
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        public async Task<StoreDTO> AddStoreAsync(StoreDTO storedto)
        {
            var store=_mapper.Map<Store>(storedto);
            var addedstore= await _storeRepository.AddStoreAsync(store);
            return _mapper.Map<StoreDTO>(addedstore);
        }

        public async Task DeleteStoreAsync(int id)
        {
            await _storeRepository.DeleteStoreAsync(id);
        }

        public async Task<IEnumerable<StoreDTO>> GetStoreAsync()
        {
            var stores = await _storeRepository.GetStoreAsync();
            return _mapper.Map<IEnumerable<StoreDTO>>(stores);
        }

        public async Task<StoreDTO> GetStoreByIDAsync(int id)
        {
            var store= await _storeRepository.GetStoreByID(id);
            return _mapper.Map<StoreDTO>(store);
        }

        public async Task<StoreDTO> UpdateStoreAsync(StoreDTO storedto)
        {
            var store = _mapper.Map<Store>(storedto);
            await _storeRepository.UpdateStoreAsync(store);
            return _mapper.Map<StoreDTO>(store);
        }
    }
}
