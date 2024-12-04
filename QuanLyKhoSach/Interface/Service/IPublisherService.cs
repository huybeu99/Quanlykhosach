using QuanLyKhoSach.DTO;

namespace QuanLyKhoSach.Interface.Service
{
    public interface IPublisherService
    {
        Task<IEnumerable<PublisherDTO>> GetPublisherAsync();
        Task<PublisherDTO> GetPublisherByIDAsync(int id);
        Task<PublisherDTO> AddPublisherAsync(PublisherDTO publisherdto);
        Task<PublisherDTO> UpdatePublisherAsync(PublisherDTO publisherdto);
        Task DeletePublisherAsync(int id);
    }
}
