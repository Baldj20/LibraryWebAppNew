using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.UseCases.BookUseCases;
using LibraryWebApp.Application.DTO;
using LibraryWebApp.Infrastructure.Exceptions;

namespace LibraryWebApp.Application.UseCases.BookUseCases
{
    public class AddBookUseCase : IAddBookUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddBookUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Add(BookDTO dto)
        {
            var book = await _unitOfWork._bookMapper.ToEntity(dto);
            var author = await _unitOfWork.Authors.GetById(dto.AuthorId);
            if (author == null)
            {
                throw new NotFoundException("Author not found");
            }
            author.AddBook(book);
            await _unitOfWork.Authors.Update(author.Id, author);
        }
    }
}
