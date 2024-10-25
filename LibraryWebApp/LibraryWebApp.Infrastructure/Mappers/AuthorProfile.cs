using AutoMapper;
using LibraryWebApp.Domain;
using LibraryWebApp.Infrastructure.Entities;

namespace LibraryWebApp.Infrastructure.Mappers
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<Author, AuthorEntity>();
            CreateMap<AuthorEntity, Author>();           
        }       
    }
}
 