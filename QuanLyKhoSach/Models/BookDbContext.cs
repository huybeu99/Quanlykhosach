using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace QuanLyKhoSach.Models
{
    public class BookDbContext:DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options)
        : base(options)
        {
        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<WareHouse> WareHouses { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BookCategory> Book_Categories { get; set; }
        public DbSet<BookImage> Images { get; set; }
        public DbSet<BookAuthor> Book_Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cấu hình khóa cho bảng nhiều-nhiều
            modelBuilder.Entity<BookAuthor>()
                .HasKey(bw => new { bw.Book_ID, bw.Author_ID });

            modelBuilder.Entity<BookCategory>()
                .HasKey(bc => new { bc.Book_ID, bc.Category_ID });

            // Cấu hình quan hệ
            modelBuilder.Entity<Book>()
                .HasOne(b => b.WareHouse)
                .WithMany(w => w.Books)
                .HasForeignKey(b => b.WareHouse_ID);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Publisher)
                .WithMany(p => p.Books)
                .HasForeignKey(b => b.Publisher_ID);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.Book_ID);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.Author_ID);

            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.BookCategories)
                .HasForeignKey(bc => bc.Book_ID);

            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BookCategories)
                .HasForeignKey(bc => bc.Category_ID);

            modelBuilder.Entity<BookImage>()
                .HasOne(i => i.Book)
                .WithMany(b => b.Images)
                .HasForeignKey(i => i.Book_ID);

            modelBuilder.Entity<WareHouse>()
                .HasOne(w => w.Store)
                .WithMany(b => b.WareHouses)
                .HasForeignKey(w => w.Store_ID);

            modelBuilder.Entity<Book>()
                .HasOne(p => p.Publisher)
                .WithMany(b => b.Books)
                .HasForeignKey(p => p.Publisher_ID);
        }
    }
}
