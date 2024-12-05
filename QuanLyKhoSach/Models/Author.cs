using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoSach.Models
{
    public class Author
    {
        [Key]
        public int Author_ID { get; set; }
        public string Author_Name { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
