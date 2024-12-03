namespace QuanLyKhoSach.Models
{
    public class BookAuthor
    {
        public int Book_ID { get; set; }
        public Book Book { get; set; }
        public int Author_ID { get; set; }
        public Author Author { get; set; }

    }
}
