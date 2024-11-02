using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.UseCases.AuthorUseCases;
using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.UseCases.AuthorUseCases
{
    public class AddAuthorUseCase : IAddAuthorUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AddAuthorUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Add(AuthorDTO dto)
        {
            var author = await _unitOfWork._authorMapper.ToEntity(dto);

            await _unitOfWork.Authors.Add(author);
        }
    }
}
