using AutoMapper;
using BookLibraryAPI.DTO;
using BookLibraryAPI.Models;

namespace BookLibraryAPI
{
    public class MappingProfillle:Profile
    {

        public MappingProfillle()    
        {

            // sorse >> destination
            CreateMap<Book, BookDTO>();

            

            CreateMap<BookDTO, Book>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Author, AuthorDto>().ReverseMap();
            
            CreateMap<Book,BookWithAuthorDto>()
                .ForMember(opt=>opt.AuthorName, opt=>opt.Ignore());

            CreateMap<Author, AutorWithBooksDto>()
            .ForMember(dest => dest.bookDtos, opt => opt.Ignore());

            CreateMap<Book, BookDto>()
            .ForMember(dest => dest.Author, opt => opt.Ignore());

        }


    }
}
