namespace QuanLyKhoSach.DTO
{
    public class PublisherDTO
    {
        public int Publisher_ID { get; set; }
        public string Publisher_Name { get; set; }
        public int Publisher_Phone { get; set; }
        public List<BookDTO> Book { get; set; }
    }
}
