using AutoMapper;
using QuanLyKhoSach.DTO;
using QuanLyKhoSach.Interface.Repository;
using QuanLyKhoSach.Interface.Service;
using QuanLyKhoSach.Models;

namespace QuanLyKhoSach.Services
{
    public class WareHouseService : IWareHouseService
    {
        private readonly IWareHouseRepository _warehouserepository;
        private readonly IMapper _mapper;
        public WareHouseService(IWareHouseRepository warehouserepository, IMapper mapper)
        {
            _warehouserepository = warehouserepository;
            _mapper = mapper;
        }

        public async Task<WareHouseDTO> AddWareHouseAsync(WareHouseDTO warehousedto)
        {
            var warehouse = _mapper.Map<WareHouse>(warehousedto);
            var addedwarehouse= await _warehouserepository.AddWareHouseAsync(warehouse);
            return _mapper.Map<WareHouseDTO>(addedwarehouse);
        }

        public async Task DeleteWareHouseAsync(int id)
        {
            await _warehouserepository.DeleteWareHouseAsync(id);
        }

        public async Task<IEnumerable<WareHouseDTO>> GetWareHouseAsync()
        {
            var warehouses= await _warehouserepository.GetWareHouseAsync();
            return _mapper.Map<IEnumerable<WareHouseDTO>>(warehouses);
        }

        public async Task<WareHouseDTO> GetWareHouseByIDAsync(int id)
        {
            var warehouse = await _warehouserepository.GetWareHouseByIDAsync(id);
            return _mapper.Map<WareHouseDTO>(warehouse);
        }

        public async Task<WareHouseDTO> UpdateWareHouseAsync(WareHouseDTO warehousedto)
        {
            var warehouse = _mapper.Map<WareHouse>(warehousedto);
            await _warehouserepository.UpdateWareHouseAsync(warehouse);
            return _mapper.Map<WareHouseDTO>(warehouse);
        }
    }
}
