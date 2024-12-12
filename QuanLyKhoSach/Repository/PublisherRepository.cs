using Microsoft.EntityFrameworkCore;
using QuanLyKhoSach.Interface.Repository;
using QuanLyKhoSach.Models;

namespace QuanLyKhoSach.Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly BookDbContext _context;
        public PublisherRepository(BookDbContext context)
        {
            _context = context;
        }

        public async Task<Publisher> AddPublisherAsync(Publisher publisher)
        {
            _context.Add(publisher);
            await _context.SaveChangesAsync();
            return publisher;
        }

        public async Task DeletePublisherAsync(int id)
        {
            var publisher = await _context.Publisher.SingleOrDefaultAsync(p => p.Publisher_ID == id);
            _context.Remove(publisher);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Publisher>> GetPublisherAsync()
        {
            return await _context.Publisher.ToListAsync();
        }

        public async Task<Publisher> GetPublisherByIDAsync(int id)
        {
            return await _context.Publisher.FirstOrDefaultAsync(p => p.Publisher_ID == id);
        }

        public async Task<Publisher> UpdatePublisherAsync(Publisher publisher)
        {
            var ExittingPublisher = await _context.Publisher.FirstOrDefaultAsync(p => p.Publisher_ID == publisher.Publisher_ID);
            ExittingPublisher.Publisher_ID = publisher.Publisher_ID;
            ExittingPublisher.Publisher_Name = publisher.Publisher_Name;
            ExittingPublisher.Publisher_Phone = publisher.Publisher_Phone;
            await _context.SaveChangesAsync();
            return ExittingPublisher;
        }
    }
}
