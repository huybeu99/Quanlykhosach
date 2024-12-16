using Microsoft.EntityFrameworkCore;
using QuanLyKhoSach.Interface.Repository;
using QuanLyKhoSach.Models;
using System.Runtime.InteropServices;

namespace QuanLyKhoSach.Repository
{
    public class StoreRepository : IStoreRepository
    {
        private readonly BookDbContext _context;
        public StoreRepository(BookDbContext context)
        {
            _context = context;
        }

        public async Task<Store> AddStoreAsync(Store store)
        {
            _context.Store.Add(store);
            await _context.SaveChangesAsync();
            return store;
        }

        public async Task DeleteStoreAsync(int id)
        {
           var store=await _context.Store.FirstOrDefaultAsync(s=>s.Store_ID==id);
            if (store != null) { 
            _context.Remove(store);
            await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Store>> GetStoreAsync()
        {
            return await _context.Store.ToListAsync();
        }

        public async Task<Store> GetStoreByIDAsync(int id)
        {
            return await _context.Store.FirstOrDefaultAsync(s => s.Store_ID == id);
        }

        public async Task<Store> UpdateStoreAsync(Store store)
        {
            var ExittingStore =await _context.Store.FirstOrDefaultAsync(s=>s.Store_ID==store.Store_ID);
            ExittingStore.Store_ID = store.Store_ID;
            ExittingStore.Store_Name = store.Store_Name;
            ExittingStore.Store_Address= store.Store_Address;

            await _context.SaveChangesAsync();
            return ExittingStore;
        }
    }
}
