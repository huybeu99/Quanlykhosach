namespace QuanLyKhoSach.DTO
{
    public class BookDTO
    {
        //Khớp với Book
        public int Book_ID { get; set; }
        public string Book_Name { get; set; }
        public DateTime Year { get; set; }
        public string Book_Description { get; set; }
        public int Book_Quantity { get; set; }

        // Các thuộc tính từ Publisher
        public string Publisher_Name { get; set; }
        public string Publisher_Phone { get; set; }

        // Các thuộc tính từ WareHouse
        public string WareHouse_Name { get; set; }
        public string WareHouse_Address { get; set; }

        // Danh sách tác giả (nếu cần)
        public List<string> Authors { get; set; }

        // Danh sách thể loại (nếu cần)
        public List<string> Categories { get; set; }

        // Danh sách hình ảnh của sách (nếu cần)
        public List<string> BookImages { get; set; }
    }
}
