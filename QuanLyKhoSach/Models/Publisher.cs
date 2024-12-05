using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoSach.Models
{
    public class Publisher
    {
        [Key]
        public int Publisher_ID { get; set; }
        public string Publisher_Name { get; set; }
        public int Publisher_Phone { get; set; }
        public List<Book> Books { get; set; }
    }
}
