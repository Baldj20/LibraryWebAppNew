using AutoMapper;
using LibraryWebApp.Application.DTO;
using LibraryWebApp.Application.UseCases.AuthorUseCases;
using LibraryWebApp.Infrastructure.Repositories;
using LibraryWebApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using LibraryWebApp.Infrastructure.Mappers;

namespace LibraryWebApp.Tests.AuthorTests
{
    public class GetAllAuthorsTest
    {
        private IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AuthorProfile());
                cfg.AddProfile(new BookProfile());
            });

            return config.CreateMapper();
        }
        [Fact]
        public async Task Get_All_Authors_Should_Get_All_Authors()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseInMemoryDatabase(databaseName: "LibraryTestDatabase")
            .Options;

            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();

            var authorsList = new List<AuthorDTO>
            {
                 new AuthorDTO
                 {
                    Id = id1,
                    Name = "Andrey",
                    Surname = "Zaitsev",
                    BirthDate = new DateTime(2004, 4, 13),
                    Country = "Belarus",
                    Books = new List<BookDTO>()
                    {
                        new BookDTO
                        {
                            ISBN = "978-3-16-148410-0",
                            Title = "Financial literacy",
                            Genre = "Self-development",
                            Description = "",
                            AuthorId = id1,
                            Count = 50
                        },
                        new BookDTO
                        {
                            ISBN = "978-0-307-74172-3",
                            Title = "Financial literacy vol.2",
                            Genre = "Self-development",
                            Description = "",
                            AuthorId = id1,
                            Count = 30
                        }
                    }
                 },

                 new AuthorDTO
                 {
                    Id = id2,
                    Name = "Dmitry",
                    Surname = "Zaitsev",
                    BirthDate = new DateTime(2004, 1, 12),
                    Country = "Belarus",
                    Books = new List<BookDTO>()
                    {
                        new BookDTO
                        {
                            ISBN = "978-1-4028-9462-6",
                            Title = "Coffee business",
                            Genre = "Self-development",
                            Description = "",
                            AuthorId = id2,
                            Count = 70
                        },
                    }
                 }
            };
            
            
            using (var context = new LibraryDbContext(options))
            {
                var unitOfWork = new UnitOfWork(context, CreateMapper());
                var addAuthorUseCase = new AddAuthorUseCase(unitOfWork);
                foreach (var author in authorsList)
                {
                    await addAuthorUseCase.Add(author);
                }            
            }

            // Act
            var list = new List<AuthorDTO>();
            using (var context = new LibraryDbContext(options))
            {
                var unitOfWork = new UnitOfWork(context, CreateMapper());
                var getAllAuthorsUseCase = new GetAllAuthorsUseCase(unitOfWork);
                list = await getAllAuthorsUseCase.GetAll();
            }

            // Assert
            using (var context = new LibraryDbContext(options))
            {
                var unitOfWork = new UnitOfWork(context, CreateMapper());

                Assert.Equal(list.Count, authorsList.Count);

                foreach (var author in list)
                {
                    Assert.Contains(authorsList, a =>
                        a.Id == author.Id &&
                        a.Name == author.Name &&
                        a.Surname == author.Surname &&
                        a.BirthDate == author.BirthDate &&
                        a.Country == author.Country &&
                        a.Books.Count == author.Books.Count &&
                        a.Books.All(b => author.Books.Any(book =>
                            book.ISBN == b.ISBN &&
                            book.Title == b.Title &&
                            book.Genre == b.Genre &&
                            book.Description == b.Description &&
                            book.Count == b.Count &&
                            book.AuthorId == b.AuthorId)));
                }
            }
        }
    }
}
