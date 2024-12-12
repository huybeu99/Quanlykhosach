using Microsoft.EntityFrameworkCore;
using QuanLyKhoSach.Interface.Repository;
using QuanLyKhoSach.Models;

namespace QuanLyKhoSach.Repository
{
    public class WareHouseRepository : IWareHouseRepository
    {
        private readonly BookDbContext _context;
        public WareHouseRepository(BookDbContext context) {
            _context = context;
        }
        public async Task<WareHouse> AddWareHouseAsync(WareHouse warehouse)
        {
            _context.Add(warehouse);
            await _context.SaveChangesAsync();
            return warehouse;
        }

        public async Task DeleteWareHouseAsync(int id)
        {
            var warehouse= await _context.WareHouse.SingleOrDefaultAsync(w => w.WareHouse_ID==id);
            _context.Remove(warehouse);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WareHouse>> GetWareHouseAsync()
        {
            return await _context.WareHouse.ToListAsync();
        }

        public async Task<WareHouse> GetWareHouseByIDAsync(int id)
        {
            return await _context.WareHouse.FirstOrDefaultAsync(w => w.WareHouse_ID == id);
        }

        public async Task<WareHouse> UpdateWareHouseAsync(WareHouse warehouse)
        {
            var ExittingWareHouse = await _context.WareHouse.FirstOrDefaultAsync(w => w.WareHouse_ID == warehouse.WareHouse_ID);
            ExittingWareHouse.WareHouse_ID= warehouse.WareHouse_ID;
            ExittingWareHouse.WareHouse_Name=warehouse.WareHouse_Name;
            ExittingWareHouse.WareHouse_Address= warehouse.WareHouse_Address;
            await _context.SaveChangesAsync();
            return ExittingWareHouse;
        }
    }
}
