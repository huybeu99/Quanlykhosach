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
            // Cấu hình mối quan hệ giữa WareHouse và Book
            modelBuilder.Entity<Book>()
                .HasOne(b => b.WareHouse)
                .WithMany(w => w.Books)
                .HasForeignKey(b => b.WareHouse_ID);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book) // Một mối quan hệ giữa Book và Book_Author
                .WithMany(b => b.BookAuthors) // Một sách có thể có nhiều tác giả
                .HasForeignKey(ba => ba.Book_ID); // Chỉ định khoá ngoại Book_ID

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author) // Một mối quan hệ giữa Author và Book_Author
                .WithMany(a => a.BookAuthors) // Một tác giả có thể viết nhiều sách
                .HasForeignKey(ba => ba.Author_ID); // Chỉ định khoá ngoại Author_ID

            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Book) // Một mối quan hệ giữa Book và Book_Category
                .WithMany(b => b.BookCategories) // Một sách có thể thuộc nhiều danh mục
                .HasForeignKey(bc => bc.Book_ID); // Chỉ định khoá ngoại Book_ID

            modelBuilder.Entity<BookCategory>()
                .HasOne(bc => bc.Category) // Một mối quan hệ giữa Category và Book_Category
                .WithMany(c => c.BookCategories) // Một danh mục có thể chứa nhiều sách
                .HasForeignKey(bc => bc.Category_ID); // Chỉ định khoá ngoại Category_ID

            // Cấu hình mối quan hệ giữa Book và BookImage
            modelBuilder.Entity<BookImage>()
                .HasOne(i => i.Book)
                .WithMany(b => b.Images)
                .HasForeignKey(i => i.Book_ID);
           
            // Cấu hình mối quan hệ giữa Store và WareHouse
            modelBuilder.Entity<WareHouse>()
                .HasOne(w => w.Store)
                .WithMany(b => b.WareHouses)
                .HasForeignKey(w => w.Store_ID);
            
            // Cấu hình mối quan hệ giữa Publisher và Book
            modelBuilder.Entity<Book>()
                .HasOne(p => p.Publisher)
                .WithMany(b => b.Books)
                .HasForeignKey(p => p.Publisher_ID);
        }
    }
}
