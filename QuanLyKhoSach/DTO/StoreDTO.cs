using System.ComponentModel.DataAnnotations;

namespace QuanLyKhoSach.DTO
{
    public class StoreDTO
    {
        public int Store_Id { get; set; }
        public string Store_Name { get; set; }
        public string Store_Address { get; set; }
        public List<WareHouseDTO> WareHouses { get; set; }  // Danh sách kho hàng (nếu cần)


    }
}
