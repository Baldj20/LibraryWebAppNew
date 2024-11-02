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
    public class UpdateAuthorTest
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
        public async Task Update_Author_Should_Return_Updated_Author()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<LibraryDbContext>()
            .UseInMemoryDatabase(databaseName: "LibraryTestDatabase")
            .Options;

            var id = Guid.NewGuid();

            var authorToUpdate = new AuthorDTO
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
                await addAuthorUseCase.Add(authorToUpdate);
            }

            // Act
            var authorWithUpdatedInfo = new AuthorDTO
            {
                Id = id,
                Name = "Dmitry",
                Surname = "Zaitsev",
                BirthDate = new DateTime(2004, 1, 12),
                Country = "Belarus",
                Books = new List<BookDTO>()
                {
                    new BookDTO
                    {
                        ISBN = "978-3-16-148410-0",
                        Title = "Coffee business",
                        Genre = "Self-development",
                        Description = "",
                        AuthorId = id,
                        Count = 150
                    }
                }
            };

            using (var context = new LibraryDbContext(options))
            {
                var unitOfWork = new UnitOfWork(context, CreateMapper());
                var updateAuthorUseCase = new UpdateAuthorUseCase(unitOfWork);
                await updateAuthorUseCase.Update(id, authorWithUpdatedInfo);
            }

            // Assert
            using (var context = new LibraryDbContext(options))
            {
                var unitOfWork = new UnitOfWork(context, CreateMapper());
                var updatedAuthor = await unitOfWork.Authors.GetById(id);

                Assert.Equal(authorToUpdate.Books.Count, updatedAuthor.Books.Count);

                Assert.Equal(authorToUpdate.Id, updatedAuthor.Id);
                Assert.Equal(authorToUpdate.Name, updatedAuthor.Name);
                Assert.Equal(authorToUpdate.Surname, updatedAuthor.Surname);
                Assert.Equal(authorToUpdate.BirthDate, updatedAuthor.BirthDate);
                Assert.Equal(authorToUpdate.Country, updatedAuthor.Country);

                for (int i = 0; i < authorToUpdate.Books.Count; i++)
                {
                    Assert.Equal(authorToUpdate.Books[i].ISBN, updatedAuthor.Books[i].ISBN);
                    Assert.Equal(authorToUpdate.Books[i].Title, updatedAuthor.Books[i].Title);
                    Assert.Equal(authorToUpdate.Books[i].Genre, updatedAuthor.Books[i].Genre);
                    Assert.Equal(authorToUpdate.Books[i].Description, updatedAuthor.Books[i].Description);
                    Assert.Equal(authorToUpdate.Books[i].AuthorId, updatedAuthor.Books[i].Author.Id);
                    Assert.Equal(authorToUpdate.Books[i].Count, updatedAuthor.Books[i].Count);
                }
            }
        }
    }
}
