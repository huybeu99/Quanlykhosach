using AutoMapper;
using QuanLyKhoSach.DTO;
using QuanLyKhoSach.Models;

namespace QuanLyKhoSach.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
           CreateMap<Book, BookDTO>()
          .ForMember(dest => dest.Book_ID, opt => opt.MapFrom(src => src.Book_ID))
          .ForMember(dest => dest.Book_Name, opt => opt.MapFrom(src => src.Book_Name))
          .ForMember(dest => dest.Book_Description, opt => opt.MapFrom(src => src.Book_Description))
          .ForMember(dest => dest.Book_Quantity, opt => opt.MapFrom(src => src.Book_Quantity))
          .ForMember(dest => dest.Publisher_Name, opt => opt.MapFrom(src => src.Publisher.Publisher_Name))
          .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.BookAuthors.Select(ba => ba.Author.Author_Name)))
          .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.BookCategories.Select(bc => bc.Category.Category_Name)));

            CreateMap<Author, AuthorDTO>()
          .ForMember(dest => dest.Author_ID, opt => opt.MapFrom(src => src.Author_ID))
          .ForMember(dest => dest.Author_Name, opt => opt.MapFrom(src => src.Author_Name))
          .ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.BookAuthors.Select(ba => ba.Book.Book_Name)));

            CreateMap<Category,CategoryDTO>()
          .ForMember(dest => dest.Category_ID, opt => opt.MapFrom(src => src.Category_ID))
          .ForMember(dest => dest.Category_Name, opt => opt.MapFrom(src => src.Category_Name))
          .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.BookCategories.Select(ba => ba.Book.Book_Name)));

            CreateMap<Publisher, PublisherDTO>()
          .ForMember(dest => dest.Publisher_ID, opt => opt.MapFrom(src => src.Publisher_ID))
          .ForMember(dest => dest.Publisher_Name, opt => opt.MapFrom(src => src.Publisher_Name))
          .ForMember(dest => dest.Publisher_Phone, opt => opt.MapFrom(src => src.Publisher_Phone))
          .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Books.Select(b=>b.Book_Name)));

            CreateMap<BookImage,BookImageDTO>()
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
