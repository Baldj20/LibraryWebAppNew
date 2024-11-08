﻿using AutoMapper;
using LibraryWebApp.Application.DTO;
using LibraryWebApp.Domain;
using LibraryWebApp.Domain.Models;
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
