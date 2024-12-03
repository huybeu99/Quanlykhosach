namespace QuanLyKhoSach.DTO
{
    public class AuthorDTO
    {
        public int Author_ID { get; set; }
        public string Author_Name { get; set; }
        public List<BookDTO> Books { get; set; }
    }
}
