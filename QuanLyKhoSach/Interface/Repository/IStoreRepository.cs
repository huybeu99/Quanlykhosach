using QuanLyKhoSach.Models;

namespace QuanLyKhoSach.Interface.Repository
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>> GetStoreAsync();
        Task<Store> GetStoreByIDAsync(int id);
        Task<Store> AddStoreAsync(Store store);
        Task<Store> UpdateStoreAsync(Store store);
        Task DeleteStoreAsync(int id);
    }
}
