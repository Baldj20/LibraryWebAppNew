using AutoMapper;
using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.DTO;
using LibraryWebApp.Domain;
using LibraryWebApp.Infrastructure.Entities;

namespace LibraryWebApp.Infrastructure.Mappers
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookEntity>();
            CreateMap<BookEntity, Book>();
        }
    }
}
