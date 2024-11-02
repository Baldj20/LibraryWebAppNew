using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.UseCases.AuthorUseCases;

namespace LibraryWebApp.Application.UseCases.AuthorUseCases
{
    public class DeleteAuthorUseCase : IDeleteAuthorUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteAuthorUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Delete(Guid id)
        {
            await _unitOfWork.Authors.Delete(id);
        }
    }
}
