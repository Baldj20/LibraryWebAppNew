using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.UseCases.BookUseCases;
using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.UseCases.BookUseCases
{
    public class UpdateBookUseCase : IUpdateBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateBookUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Update(string isbn, BookDTO dto)
        {
            var book = await _unitOfWork._bookMapper.ToEntity(dto);

            await _unitOfWork.Books.Update(isbn, book);
        }
    }
}
