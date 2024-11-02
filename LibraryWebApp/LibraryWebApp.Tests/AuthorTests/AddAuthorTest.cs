using AutoMapper;
using LibraryWebApp.Application.DTO;
using LibraryWebApp.Application.UseCases.AuthorUseCases;
using LibraryWebApp.Infrastructure;
using LibraryWebApp.Infrastructure.Mappers;
using LibraryWebApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LibraryWebApp.Tests.AuthorTests
{
    public class AddAuthorTest
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
        public async Task AddAuthor_Should_Add_New_Author()
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

            // Act
            using (var context = new LibraryDbContext(options))
            {
                var unitOfWork = new UnitOfWork(context, CreateMapper());
                var addAuthorUseCase = new AddAuthorUseCase(unitOfWork);
                await addAuthorUseCase.Add(newAuthor);
            }

            // Assert
            using (var context = new LibraryDbContext(options))
            {
                var unitOfWork = new UnitOfWork(context, CreateMapper());
                var addedAuthor = await unitOfWork.Authors.GetById(id);

                Assert.NotNull(addedAuthor);
                Assert.Equal(newAuthor.Name, addedAuthor.Name);
                Assert.Equal(newAuthor.Surname, addedAuthor.Surname);
                Assert.Equal(newAuthor.BirthDate, addedAuthor.BirthDate);
                Assert.Equal(newAuthor.Country, addedAuthor.Country);

                for (int i = 0; i < addedAuthor.Books.Count; i++)
                {
                    Assert.Equal(addedAuthor.Books[i].ISBN, newAuthor.Books[i].ISBN);
                    Assert.Equal(addedAuthor.Books[i].Title, newAuthor.Books[i].Title);
                    Assert.Equal(addedAuthor.Books[i].Genre, newAuthor.Books[i].Genre);
                    Assert.Equal(addedAuthor.Books[i].Description, newAuthor.Books[i].Description);
                    Assert.Equal(addedAuthor.Books[i].Count, newAuthor.Books[i].Count);
                    Assert.Equal(addedAuthor.Books[i].Author.Id, newAuthor.Books[i].AuthorId);
                }
            }
        }
    }
}
