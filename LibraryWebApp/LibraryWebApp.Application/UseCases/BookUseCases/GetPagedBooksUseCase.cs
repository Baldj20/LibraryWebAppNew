using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.UseCases.BookUseCases;
using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.UseCases.BookUseCases
{
    public class GetPagedBooksUseCase : IGetPagedBooksUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPagedBooksUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<BookDTO>> GetPagedBooks(PaginationParams paginationParams)
        {
            var pagedBooks = await _unitOfWork.Books.GetPaged(paginationParams);

            var pagedBooksDTO = new List<BookDTO>();

            foreach (var book in pagedBooks.Items)
            {
                pagedBooksDTO.Add(_unitOfWork._bookMapper.ToDTO(book));
            }

            return pagedBooksDTO;
        }
    }
}
