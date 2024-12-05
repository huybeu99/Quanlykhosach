using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoSach.Models
{
    public class Store
    {
        [Key]
        public int Store_ID { get; set; }
        public string Store_Name { get; set; }
        public string Store_Address { get; set; }
        public ICollection<WareHouse> WareHouses { get; set; }
    }
}
