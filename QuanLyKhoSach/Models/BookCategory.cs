namespace QuanLyKhoSach.Models
{
    public class BookCategory
    {
        public int Book_ID { get; set; }
        public Book Book { get; set; }
        public int Category_ID { get; set; }
        public Category Category { get; set; }
    }
}
