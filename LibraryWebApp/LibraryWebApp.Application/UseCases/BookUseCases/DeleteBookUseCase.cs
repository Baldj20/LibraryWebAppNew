using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.UseCases.BookUseCases;

namespace LibraryWebApp.Application.UseCases.BookUseCases
{
    public class DeleteBookUseCase : IDeleteBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteBookUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Delete(string isbn)
        {
            await _unitOfWork.Books.Delete(isbn);

        }
    }
}
