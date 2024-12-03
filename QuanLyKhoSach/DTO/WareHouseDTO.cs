namespace QuanLyKhoSach.DTO
{
    public class WareHouseDTO
    {
        public int WareHouse_ID { get; set; }  // Khóa chính của kho hàng
        public string WareHouse_Name { get; set; }  // Tên kho hàng
        public string WareHouse_Address { get; set; }  // Địa chỉ kho hàng
        public List<BookDTO> Books { get; set; }
    }
}
