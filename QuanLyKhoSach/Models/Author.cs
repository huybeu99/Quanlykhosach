namespace QuanLyKhoSach.Models
{
    public class Author
    {
        public int Author_ID { get; set; }
        public string Author_Name { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
    }
}
