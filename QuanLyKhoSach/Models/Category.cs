﻿namespace QuanLyKhoSach.Models
{
    public class Category
    {
        public int Category_ID { get; set; }
        public string Category_Name { get; set; }
        public List<BookCategory> BookCategories { get; set; }
    }
}