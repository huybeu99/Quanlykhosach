using QuanLyKhoSach.DTO;
using QuanLyKhoSach.Models;

namespace QuanLyKhoSach.Interface.Service
{
    public interface IStoreService
    {
        Task<IEnumerable<StoreDTO>> GetStoreAsync();
        Task<StoreDTO> GetStoreByIDAsync(int id);
        Task<StoreDTO> AddStoreAsync(StoreDTO storedto);
        Task<StoreDTO> UpdateStoreAsync(StoreDTO storedto);
        Task DeleteStoreAsync(int id);
    }
}
