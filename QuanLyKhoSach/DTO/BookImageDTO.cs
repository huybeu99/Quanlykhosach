namespace QuanLyKhoSach.DTO
{
    public class BookImageDTO
    {
        public int Image_ID { get; set; }
        public string Image_Url { get; set; }
        public string Alt_Text { get; set; }
        public List<string> Book { get; set; }
    }
}
