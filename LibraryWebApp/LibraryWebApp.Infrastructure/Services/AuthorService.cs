using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.Services;
using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Infrastructure.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Add(AuthorDTO dto)
        {
            var author = await _unitOfWork._authorMapper.ToEntity(dto);

            await _unitOfWork.Authors.Add(author);
        }

        public async Task Delete(Guid id)
        {
            await _unitOfWork.Authors.Delete(id);
        }

        public async Task<List<AuthorDTO>> GetAll()
        {
            var authorDTOList = new List<AuthorDTO>();

            var authors = await _unitOfWork.Authors.GetAll();

            foreach (var author in authors)
            {
                authorDTOList.Add(_unitOfWork._authorMapper.ToDTO(author));
            }

            return authorDTOList;
        }

        public async Task<List<BookDTO>> GetBooks(Guid authorId)
        {
            var bookList = await _unitOfWork.Authors.GetBooks(authorId);

            var bookDTOList = new List<BookDTO>();

            foreach (var book in bookList)
            {
                bookDTOList.Add(_unitOfWork._bookMapper.ToDTO(book));
            }

            return bookDTOList;
        }

        public async Task<AuthorDTO> GetById(Guid id)
        {
            var author = await _unitOfWork.Authors.GetById(id);
            return _unitOfWork._authorMapper.ToDTO(author);
        }

        public async Task Update(Guid id, AuthorDTO dto)
        {
            var author = await _unitOfWork._authorMapper.ToEntity(dto);

            await _unitOfWork.Authors.Update(id, author);
        }
    }
}
