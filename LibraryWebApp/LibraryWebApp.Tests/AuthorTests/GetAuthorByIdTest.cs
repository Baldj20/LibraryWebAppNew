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
    public class GetAuthorByIdTest
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
        public async Task Get_Author_By_Id_Should_Return_Author()
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
            var author = new AuthorDTO();

            using (var context = new LibraryDbContext(options))
            {
                var unitOfWork = new UnitOfWork(context, CreateMapper());
                var getAuthorByIdCase = new GetAuthorByIdUseCase(unitOfWork);
                author = await getAuthorByIdCase.GetById(id);
            }

            // Assert
            using (var context = new LibraryDbContext(options))
            {
                var unitOfWork = new UnitOfWork(context, CreateMapper());

                Assert.Equal(author.Books.Count, newAuthor.Books.Count);

                Assert.Equal(author.Id, newAuthor.Id);
                Assert.Equal(author.Name, newAuthor.Name);
                Assert.Equal(author.Surname, newAuthor.Surname);
                Assert.Equal(author.BirthDate, newAuthor.BirthDate);
                Assert.Equal(author.Country, newAuthor.Country);

                for (int i = 0; i < author.Books.Count; i++)
                {
                    Assert.Equal(author.Books[i].ISBN, newAuthor.Books[i].ISBN);
                    Assert.Equal(author.Books[i].Title, newAuthor.Books[i].Title);
                    Assert.Equal(author.Books[i].Genre, newAuthor.Books[i].Genre);
                    Assert.Equal(author.Books[i].Description, newAuthor.Books[i].Description);
                    Assert.Equal(author.Books[i].AuthorId, newAuthor.Books[i].AuthorId);
                    Assert.Equal(author.Books[i].Count, newAuthor.Books[i].Count);
                }
            }
        }

        [Fact]
        public async Task Get_Author_By_Id_Should_Return_Null()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseInMemoryDatabase(databaseName: "LibraryTestDatabase")
            .Options;

            var id = Guid.NewGuid();

            // Act

            var author = new AuthorDTO();

            using (var context = new LibraryDbContext(options))
            {
                var unitOfWork = new UnitOfWork(context, CreateMapper());
                var getAuthorByIdUseCase = new GetAuthorByIdUseCase(unitOfWork);
                author = await getAuthorByIdUseCase.GetById(id);
            }

            // Assert
            using (var context = new LibraryDbContext(options))
            {
                var unitOfWork = new UnitOfWork(context, CreateMapper());

                Assert.Null(author);
            }
        }
    }
}
