using AutoMapper;
using LibraryWebApp.Application.Abstractions.Mappers;
using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.Services;
using LibraryWebApp.Application.DTO;
using LibraryWebApp.Domain;
using LibraryWebApp.Infrastructure.Exceptions;
using LibraryWebApp.Infrastructure.Mappers;

namespace LibraryWebApp.Infrastructure.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorMapper _authorMapper;
        private readonly IBookMapper _bookMapper;
        public AuthorService(IAuthorRepository authorRepository, IBookRepository bookRepository, 
            IAuthorMapper authorMapper, IBookMapper bookMapper)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _authorMapper = authorMapper;
            _bookMapper = bookMapper;
        }
        public async Task Add(AuthorDTO dto)
        {
            //var books = new List<Book>();
            //foreach (var isbn in dto.BooksISBN)
            //{
            //    books.Add(await _bookRepository.GetByISBN(isbn));
            //}

            //var author = new Author(dto.Id, dto.Name, dto.Surname, 
            //    dto.BirthDate, dto.Country, books);
            var author = await _authorMapper.ToEntity(dto);
            //var books = author.Books;
            //foreach (var book in books)
            //{
            //    try
            //    {
            //        var bookFromRepos = await _bookRepository.GetByISBN(book.ISBN);
            //    }         
            //    catch(NotFoundException ex)
            //    {
            //        await _bookRepository.Add(book);
            //    }                    
            //}
            await _authorRepository.Add(author);

            //foreach (var book in author.Books)
            //{
            //    await _bookRepository.Add(book);
            //}
            
        }

        public async Task Delete(Guid id)
        {
            await _authorRepository.Delete(id);
        }

        public async Task<List<AuthorDTO>> GetAll()
        {
            //var authors = await _authorRepository.GetAll();

            //var authorDTOList = new List<AuthorDTO>();

            //for (int i = 0; i < authors.Count; i++)
            //{
            //    var authorBooksISBN = new List<string>();
            //    foreach (var book in authors[i].Books)
            //    {
            //        authorBooksISBN.Add(book.ISBN);
            //    }

            //    var authorDTO = new AuthorDTO
            //    {
            //        Id = authors[i].Id,
            //        Name = authors[i].Name,
            //        Surname = authors[i].Surname,
            //        BirthDate = authors[i].BirthDate,
            //        Country = authors[i].Country,
            //        BooksISBN = authorBooksISBN
            //    };

            //    authorDTOList.Add(authorDTO);
            //}
            var authorDTOList = new List<AuthorDTO>();

            var authors = await _authorRepository.GetAll();

            foreach (var author in authors)
            {
                authorDTOList.Add(_authorMapper.ToDTO(author));
            }

            return authorDTOList;
        }

        public async Task<List<BookDTO>> GetBooks(Guid authorId)
        {
            var bookList = await _authorRepository.GetBooks(authorId);

            var bookDTOList = new List<BookDTO>();

            foreach (var book in bookList)
            {
                bookDTOList.Add(_bookMapper.ToDTO(book));
            }

            return bookDTOList;
        }

        public async Task<AuthorDTO> GetById(Guid id)
        {

            //var author = await _authorRepository.GetById(id);

            //var books = new List<Book>();
            //foreach (var isbn in dto.BooksISBN)
            //{
            //    books.Add(await _bookRepository.GetByISBN(isbn));
            //}

            //var author = new Author(dto.Id, dto.Name, dto.Surname,
            //    dto.BirthDate, dto.Country, books);
            var author = await _authorRepository.GetById(id);
            return _authorMapper.ToDTO(author);
        }

        public async Task Update(Guid id, AuthorDTO dto)
        {
            var author = await _authorMapper.ToEntity(dto);

            await _authorRepository.Update(id, author);
        }
    }
}
