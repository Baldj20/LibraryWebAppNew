using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.UseCases.AuthorUseCases;
using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.UseCases.AuthorUseCases
{
    public class UpdateAuthorUseCase : IUpdateAuthorUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAuthorUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Update(Guid id, AuthorDTO dto)
        {
            var author = await _unitOfWork._authorMapper.ToEntity(dto);

            await _unitOfWork.Authors.Update(id, author);
        }
    }
}
