using QuanLyKhoSach.DTO;

namespace QuanLyKhoSach.Interface.Service
{
    public interface IWareHouseService
    {
        Task<IEnumerable<WareHouseDTO>> GetWareHouseAsync();
        Task<WareHouseDTO> GetWareHouseByIDAsync(int id);
        Task<WareHouseDTO> AddWareHouseAsync(WareHouseDTO warehousedto);
        Task<WareHouseDTO> UpdateWareHouseAsync(WareHouseDTO warehousedto);
        Task DeleteWareHouseAsync(int id);
    }
}
