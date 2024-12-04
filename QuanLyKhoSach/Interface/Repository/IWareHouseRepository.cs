using QuanLyKhoSach.Models;

namespace QuanLyKhoSach.Interface.Repository
{
    public interface IWareHouseRepository
    {
        Task<IEnumerable<WareHouse>> GetWareHouseAsync();
        Task<WareHouse> GetWareHouseByIDAsync(int id);
        Task<WareHouse> AddWareHouseAsync(WareHouse warehouse);
        Task<WareHouse> UpdateWareHouseAsync(WareHouse warehouse);
        Task DeleteWareHouseAsync(int id);
    }
}
