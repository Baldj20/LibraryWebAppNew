using LibraryWebApp.Application.Abstractions.Repositories;
using LibraryWebApp.Application.Abstractions.UseCases.AuthorUseCases;
using LibraryWebApp.Application.DTO;

namespace LibraryWebApp.Application.UseCases.AuthorUseCases
{
    public class GetPagedAuthorsUseCase : IGetPagedAuthorsUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPagedAuthorsUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<AuthorDTO>> GetPagedAuthors(PaginationParams paginationParams)
        {
            var pagedAuthors = await _unitOfWork.Authors.GetPaged(paginationParams);

            var pagedAuthorsDTO = new List<AuthorDTO>();

            foreach (var author in pagedAuthors.Items)
            {
                pagedAuthorsDTO.Add(_unitOfWork._authorMapper.ToDTO(author));
            }

            return pagedAuthorsDTO;
        }
    }
}
