using AutoMapper;
using LibraryWebApp.Application.DTO;
using LibraryWebApp.Domain.Models;
using LibraryWebApp.Infrastructure.Entities;

namespace LibraryWebApp.Infrastructure.Mappers
{
    public class RefreshTokenProfile : Profile
    {
        public RefreshTokenProfile()
        {
            CreateMap<RefreshToken, RefreshTokenEntity>();
            CreateMap<RefreshTokenEntity, RefreshToken>();
        }
    }
}
