using AutoMapper;
using QuanLyKhoSach.DTO;
using QuanLyKhoSach.Models;

namespace QuanLyKhoSach.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
               CreateMap<Book, BookDTO>()
                // Direct properties
                .ForMember(dest => dest.Book_ID, opt => opt.MapFrom(src => src.Book_ID))
                .ForMember(dest => dest.Book_Name, opt => opt.MapFrom(src => src.Book_Name))
                .ForMember(dest => dest.Book_Description, opt => opt.MapFrom(src => src.Book_Description))
                .ForMember(dest => dest.Book_Quantity, opt => opt.MapFrom(src => src.Book_Quantity))
                .ForMember(dest => dest.Book_Year, opt => opt.MapFrom(src => src.Book_Year))
                
                // Publisher properties
                .ForMember(dest => dest.Publisher_ID, opt => opt.MapFrom(src => src.Publisher_ID))
                .ForMember(dest => dest.Publisher_Name, opt => opt.MapFrom(src => src.Publisher != null ? src.Publisher.Publisher_Name : null))
                .ForMember(dest => dest.Publisher_Phone, opt => opt.MapFrom(src => src.Publisher != null ? src.Publisher.Publisher_Phone : null))
                
                // WareHouse properties
                .ForMember(dest => dest.WareHouse_ID, opt => opt.MapFrom(src => src.WareHouse_ID))
                .ForMember(dest => dest.WareHouse_Name, opt => opt.MapFrom(src => src.WareHouse != null ? src.WareHouse.WareHouse_Name : null))
                .ForMember(dest => dest.WareHouse_Address, opt => opt.MapFrom(src => src.WareHouse != null ? src.WareHouse.WareHouse_Address : null))
                
                // Author collection
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => 
                    src.BookAuthor != null ? src.BookAuthor.Select(ba => new BookDTO.AuthorDetail
                    {
                        Author_ID = ba.Author.Author_ID,
                        Author_Name = ba.Author.Author_Name
                    }).ToList() : new List<BookDTO.AuthorDetail>()))
                
                // Category collection
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => 
                    src.BookCategory != null ? src.BookCategory.Select(bc => new BookDTO.CategoryDetail
                    {
                        Category_ID = bc.Category.Category_ID,
                        Category_Name = bc.Category.Category_Name
                    }).ToList() : new List<BookDTO.CategoryDetail>()));

            // BookDTO -> Book mapping
            CreateMap<BookDTO, Book>()
                // Direct properties
                .ForMember(dest => dest.Book_ID, opt => opt.MapFrom(src => src.Book_ID))
                .ForMember(dest => dest.Book_Name, opt => opt.MapFrom(src => src.Book_Name))
                .ForMember(dest => dest.Book_Description, opt => opt.MapFrom(src => src.Book_Description))
                .ForMember(dest => dest.Book_Quantity, opt => opt.MapFrom(src => src.Book_Quantity))
                .ForMember(dest => dest.Book_Year, opt => opt.MapFrom(src => src.Book_Year))
                .ForMember(dest => dest.Publisher_ID, opt => opt.MapFrom(src => src.Publisher_ID))
                .ForMember(dest => dest.WareHouse_ID, opt => opt.MapFrom(src => src.WareHouse_ID))
                
                // Map collections
                .ForMember(dest => dest.BookAuthor, opt => opt.MapFrom(src => 
                    src.Author.Select(a => new BookAuthor 
                    {
                        Book_ID = src.Book_ID,
                        Author_ID = a.Author_ID
                    }).ToList()))
                .ForMember(dest => dest.BookCategory, opt => opt.MapFrom(src => 
                    src.Category.Select(c => new BookCategory
                    {
                        Book_ID = src.Book_ID,
                        Category_ID = c.Category_ID
                    }).ToList()));
            
            CreateMap<Author, AuthorDTO>()
          .ForMember(dest => dest.Author_ID, opt => opt.MapFrom(src => src.Author_ID))
          .ForMember(dest => dest.Author_Name, opt => opt.MapFrom(src => src.Author_Name))
          .ForMember(dest => dest.BookID, opt => opt.MapFrom(src => src.BookAuthors.Select(ba => ba.Book.Book_ID)));

            CreateMap<AuthorDTO, Author>()
                .ForMember(dest => dest.BookAuthors, opt => opt.Ignore())
                .ForMember(dest => dest.Author_ID, opt => opt.MapFrom(src => src.Author_ID))
                .ForMember(dest => dest.Author_Name, opt => opt.MapFrom(src => src.Author_Name));
            
            CreateMap<Category, CategoryDTO>()
          .ForMember(dest => dest.Category_ID, opt => opt.MapFrom(src => src.Category_ID))
          .ForMember(dest => dest.Category_Name, opt => opt.MapFrom(src => src.Category_Name))
          .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.BookCategories.Select(ba => ba.Book.Book_Name)));

            CreateMap<Publisher, PublisherDTO>()
          .ForMember(dest => dest.Publisher_ID, opt => opt.MapFrom(src => src.Publisher_ID))
          .ForMember(dest => dest.Publisher_Name, opt => opt.MapFrom(src => src.Publisher_Name))
          .ForMember(dest => dest.Publisher_Phone, opt => opt.MapFrom(src => src.Publisher_Phone))
          .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Books.Select(b => b.Book_Name)));

            CreateMap<BookImage, BookImageDTO>()
          .ForMember(dest => dest.Image_ID, opt => opt.MapFrom(src => src.Image_ID))
          .ForMember(dest => dest.Image_Url, opt => opt.MapFrom(src => src.Image_Url))
          .ForMember(dest => dest.Alt_Text, opt => opt.MapFrom(src => src.Alt_Text))
          .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book.Book_Name));

            CreateMap<Store, StoreDTO>()
          .ForMember(dest => dest.Store_Id, opt => opt.MapFrom(src => src.Store_ID))
          .ForMember(dest => dest.Store_Name, opt => opt.MapFrom(src => src.Store_Name))
          .ForMember(dest => dest.Store_Address, opt => opt.MapFrom(src => src.Store_Address))
          .ForMember(dest => dest.WareHouses, opt => opt.MapFrom(src => src.WareHouses.Select(wh => wh.WareHouse_Name)));

            CreateMap<WareHouse, WareHouseDTO>()
           .ForMember(dest => dest.WareHouse_ID, opt => opt.MapFrom(src => src.WareHouse_ID))
          .ForMember(dest => dest.WareHouse_Name, opt => opt.MapFrom(src => src.WareHouse_Name))
          .ForMember(dest => dest.WareHouse_Address, opt => opt.MapFrom(src => src.WareHouse_Address))
          .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.Books.Select(b => b.Book_Name)));

        }
    }
}
