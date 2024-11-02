using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.UseCases.AuthorUseCases;
using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.UseCases.AuthorUseCases
{
    public class GetAuthorBooksUseCase : IGetAuthorBooksUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAuthorBooksUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
    }
}
