using AutoMapper;
using BookLibraryAPI.DTO;
using BookLibraryAPI.Models;

namespace BookLibraryAPI
{
    public class MappingProfillle:Profile
    {

        public MappingProfillle()    
        {

            CreateMap<Book, BookDTO>();

            CreateMap<BookDTO, Book>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

        }


    }
}
