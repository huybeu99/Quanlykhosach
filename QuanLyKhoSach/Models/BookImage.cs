using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoSach.Models
{
    public class BookImage
    {
        [Key]
        public int Image_ID { get; set; }
        public int Book_ID { get; set; }
        public string Image_Url { get; set; }
        public string Alt_Text { get; set; }
        public Book Book { get; set; }
    }
}
