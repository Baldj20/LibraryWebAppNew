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
    public class GetAuthorBooksTest
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
        public async Task GetAuthorBooks_Should_Return_Author_Books()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseInMemoryDatabase(databaseName: "LibraryTestDatabase")
            .Options;

            var id = Guid.NewGuid();
            var newAuthor = new AuthorDTO
            {
                Id = id,
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
                        AuthorId = id,
                        Count = 50
                    },
                    new BookDTO
                    {
                        ISBN = "978-0-307-74172-3",
                        Title = "Financial literacy vol.2",
                        Genre = "Self-development",
                        Description = "",
                        AuthorId = id,
                        Count = 30
                    }
                }
            };

            using (var context = new LibraryDbContext(options))
            {
                var unitOfWork = new UnitOfWork(context, CreateMapper());
                var addAuthorUseCase = new AddAuthorUseCase(unitOfWork);
                await addAuthorUseCase.Add(newAuthor);
            }

            // Act
            var books = new List<BookDTO>();

            using (var context = new LibraryDbContext(options))
            {
                var unitOfWork = new UnitOfWork(context, CreateMapper());
                var getAuthorBooksUseCase = new GetAuthorBooksUseCase(unitOfWork);
                books = await getAuthorBooksUseCase.GetBooks(id);
            }

            // Assert
            using (var context = new LibraryDbContext(options))
            {
                var unitOfWork = new UnitOfWork(context, CreateMapper());

                var authorBooks = newAuthor.Books;

                Assert.Equal(authorBooks.Count, books.Count);

                foreach (var book in authorBooks)
                {
                    Assert.Contains(authorBooks, authorBook =>
                    authorBook.ISBN == book.ISBN &&
                    authorBook.Title == book.Title &&
                    authorBook.Genre == book.Genre &&
                    authorBook.Description == book.Description &&
                    authorBook.AuthorId == book.AuthorId &&
                    authorBook.Count == book.Count);
                }
            }
        }
    }
}
