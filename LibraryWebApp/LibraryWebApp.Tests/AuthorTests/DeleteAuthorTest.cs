using LibraryWebApp.Application.DTO;
using LibraryWebApp.Application.UseCases.AuthorUseCases;
using LibraryWebApp.Infrastructure.Repositories;
using LibraryWebApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using AutoMapper;
using LibraryWebApp.Infrastructure.Mappers;

namespace LibraryWebApp.Tests.AuthorTests
{
    public class DeleteAuthorTest
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
        public async Task DeleteAuthor_Should_Delete_Author_And_His_Books()
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
            using (var context = new LibraryDbContext(options))
            {
                var unitOfWork = new UnitOfWork(context, CreateMapper());
                var deleteAuthorUseCase = new DeleteAuthorUseCase(unitOfWork);
                await deleteAuthorUseCase.Delete(id);
            }

            // Assert
            using (var context = new LibraryDbContext(options))
            {
                var unitOfWork = new UnitOfWork(context, CreateMapper());

                var deletedAuthor = await unitOfWork.Authors.GetById(id);

                Assert.Null(deletedAuthor);

                foreach (var book in newAuthor.Books)
                {
                    Assert.Null(await unitOfWork.Books.GetByISBN(book.ISBN));
                }
            }
        }
    }
}
