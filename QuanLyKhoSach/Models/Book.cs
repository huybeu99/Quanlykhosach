﻿using static System.Net.Mime.MediaTypeNames;

namespace QuanLyKhoSach.Models
{
    public class Book
    {
        //Khóa Chính
        public int Book_ID { get; set; }
        //Thuộc tính cơ bản
        public string Book_Name { get; set; }
        public int Year { get; set; }
        public string Book_Description { get; set; }
        public int Book_Quantity { get; set; }
        //Khóa ngoại
        public int WareHouse_ID { get; set; }
        public int Publisher_ID { get; set; }
        //Mối quan hệ điều hướng
        public WareHouse WareHouse { get; set; }
        public Publisher Publisher { get; set; }
        //Quan hệ nhiều-nhiều
        public ICollection<BookAuthor> BookAuthors { get; set; }
        public ICollection<BookCategory> BookCategories { get; set; }
        //Quan hệ một-nhiều
        public ICollection<BookImage> Images { get; set; }
    }
}