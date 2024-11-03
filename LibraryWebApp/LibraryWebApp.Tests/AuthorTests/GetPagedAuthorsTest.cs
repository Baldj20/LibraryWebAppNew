using AutoMapper;
using LibraryWebApp.Application.DTO;
using LibraryWebApp.Application.UseCases.AuthorUseCases;
using LibraryWebApp.Infrastructure.Repositories;
using LibraryWebApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Xunit;
using LibraryWebApp.Infrastructure.Mappers;
using LibraryWebApp.Application;

namespace LibraryWebApp.Tests.AuthorTests
{
    public class GetPagedAuthorsTest
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
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            var id1 = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            var id3 = Guid.NewGuid();

            var newAuthor1 = new AuthorDTO
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
            };

            var newAuthor2 = new AuthorDTO
            {
                Id = id2,
                Name = "Dmitry",
                Surname = "Zaitsev",
                BirthDate = new DateTime(2004, 4, 13),
                Country = "Belarus",
                Books = new List<BookDTO>()
                {
                    new BookDTO
                    {
                        ISBN = "978-3-16-148410-1",
                        Title = "Financial literacy",
                        Genre = "Self-development",
                        Description = "",
                        AuthorId = id2,
                        Count = 50
                    },
                    new BookDTO
                    {
                        ISBN = "978-0-307-74172-4",
                        Title = "Financial literacy vol.2",
                        Genre = "Self-development",
                        Description = "",
                        AuthorId = id2,
                        Count = 30
                    }
                }
            };

            var newAuthor3 = new AuthorDTO
            {
                Id = id3,
                Name = "Roman",
                Surname = "Zaitsev",
                BirthDate = new DateTime(2004, 4, 13),
                Country = "Belarus",
                Books = new List<BookDTO>()
                {
                    new BookDTO
                    {
                        ISBN = "978-3-16-148410-5",
                        Title = "Financial literacy",
                        Genre = "Self-development",
                        Description = "",
                        AuthorId = id3,
                        Count = 50
                    },
                    new BookDTO
                    {
                        ISBN = "978-0-307-74172-9",
                        Title = "Financial literacy vol.2",
                        Genre = "Self-development",
                        Description = "",
                        AuthorId = id3,
                        Count = 30
                    }
                }
            };
            var authors = new List<AuthorDTO> { newAuthor1, newAuthor2, newAuthor3 };

            var paginationParams = new PaginationParams
            {
                PageSize = 2,
                PageNumber = 2
            };

            using (var context = new LibraryDbContext(options))
            {
                var unitOfWork = new UnitOfWork(context, CreateMapper());
                var addAuthorUseCase = new AddAuthorUseCase(unitOfWork);
                foreach (var author in authors)
                {
                    await addAuthorUseCase.Add(author);
                }
            }


            // Act
            var pagedAuthors = new List<AuthorDTO>();

            using (var context = new LibraryDbContext(options))
            {
                var unitOfWork = new UnitOfWork(context, CreateMapper());
                var getPagedAuthorsUseCase = new GetPagedAuthorsUseCase(unitOfWork);
                pagedAuthors = await getPagedAuthorsUseCase.GetPagedAuthors(paginationParams);
            }

            // Assert
            using (var context = new LibraryDbContext(options))
            {
                var unitOfWork = new UnitOfWork(context, CreateMapper());

                var result = authors.Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                    .Take(paginationParams.PageSize).ToList();


                Assert.Equal(result.Count, pagedAuthors.Count);

                for (int i = 0; i < result.Count; i++)
                {
                    Assert.Equal(result[i].Name, pagedAuthors[i].Name);
                    Assert.Equal(result[i].Surname, pagedAuthors[i].Surname);
                    Assert.Equal(result[i].BirthDate, pagedAuthors[i].BirthDate);
                    Assert.Equal(result[i].Country, pagedAuthors[i].Country);
                    Assert.Equal(result[i].Books.Count, pagedAuthors[i].Books.Count);

                    for (int j = 0; j < result[i].Books.Count; j++)
                    {
                        Assert.Contains(pagedAuthors[i].Books, b =>
                        b.ISBN == result[i].Books[j].ISBN &&
                        b.Title == result[i].Books[j].Title &&
                        b.Genre == result[i].Books[j].Genre &&
                        b.Description == result[i].Books[j].Description &&
                        b.Count == result[i].Books[j].Count &&
                        b.AuthorId == result[i].Books[j].AuthorId);
                    }
                }
                
            }
        }
    }
}
