using AutoMapper;
using LibraryWebApp.Domain.Models;
using LibraryWebApp.Infrastructure.Entities;

namespace LibraryWebApp.Infrastructure.Mappers
{
    public class UserBookProfile : Profile
    {
        public UserBookProfile()
        {
            CreateMap<UserBook, UserBookEntity>();
            CreateMap<UserBookEntity, UserBook>();
        }
    }
}
