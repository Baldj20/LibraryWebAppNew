using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.UseCases.BookUseCases;
using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.UseCases.BookUseCases
{
    public class GetAllBooksUseCase : IGetAllBooksUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllBooksUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<BookDTO>> GetAll()
        {
            var bookDTOList = new List<BookDTO>();

            var books = await _unitOfWork.Books.GetAll();

            foreach (var book in books)
            {
                bookDTOList.Add(_unitOfWork._bookMapper.ToDTO(book));
            }

            return bookDTOList;
        }
    }
}
