using QuanLyKhoSach.Models;

namespace QuanLyKhoSach.Interface.Repository
{
    public interface IPublisherRepository
    {
        Task<IEnumerable<Publisher>> GetPublisherAsync();
        Task<Publisher> GetPublisherByIDAsync(int id);
        Task<Publisher> AddPublisherAsync(Publisher publisher);
        Task<Publisher> UpdatePublisherAsync(Publisher publisher);
        Task DeletePublisherAsync(int id);
    }
}
