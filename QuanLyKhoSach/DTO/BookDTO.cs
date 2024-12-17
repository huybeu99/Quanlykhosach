namespace QuanLyKhoSach.DTO
{
    public class BookDTO
    {
        //Khớp với Book
        public int Book_ID { get; set; }
        public string Book_Name { get; set; }
        public int Book_Year { get; set; }
        public string Book_Description { get; set; }
        public int Book_Quantity { get; set; }

        // Các thuộc tính từ Publisher
        public string Publisher_Name { get; set; }
        public string Publisher_Phone { get; set; }
        public int Publisher_ID { get; set; }

        // Các thuộc tính từ WareHouse
        public string WareHouse_Name { get; set; }
        public string WareHouse_Address { get; set; }
        public int WareHouse_ID { get; set; }


        // Danh sách tác giả (nếu cần)
        public List<AuthorDetail> Author { get; set; }

        // Danh sách thể loại (nếu cần)
        public List<CategoryDetail> Category { get; set; }
        public class AuthorDetail
        {
            public int Author_ID { get; set; }
            public string Author_Name { get; set; }
        }
        public class CategoryDetail
        {
            public int Category_ID { get; set; }
            public string Category_Name { get; set; }
        }
    }

    //// Danh sách hình ảnh của sách (nếu cần)
    //public List<string> BookImage { get; set; }
}

