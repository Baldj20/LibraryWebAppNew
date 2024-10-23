using AutoMapper;
using LibraryWebApp.Domain;
using LibraryWebApp.Infrastructure.Entities;

namespace LibraryWebApp.Infrastructure.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserEntity>()
                .ForMember(dest => dest.Role,
                 opt => opt.MapFrom(src => src.Role == Role.User ? 1 : 0));
            CreateMap<UserEntity, User>()
                .ForMember(dest => dest.Role, 
                 opt => opt.MapFrom(src => src.Role == 0 ? Role.Admin : Role.User));
        }
    }
}
