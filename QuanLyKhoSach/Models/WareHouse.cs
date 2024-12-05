using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoSach.Models
{
    public class WareHouse
    {
        [Key]
        public int WareHouse_ID { get; set; }
        public string WareHouse_Name { get; set; }
        public string WareHouse_Address { get; set;}
        public int Store_ID { get; set; }
        public Store Store { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
