using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.UseCases.BookUseCases;
using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.UseCases.BookUseCases
{
    public class GetBookByISBNUseCase : IGetBookByISBNUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetBookByISBNUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<BookDTO> GetByISBN(string isbn)
        {
            var book = await _unitOfWork.Books.GetByISBN(isbn);
            return _unitOfWork._bookMapper.ToDTO(book);
        }
    }
}
