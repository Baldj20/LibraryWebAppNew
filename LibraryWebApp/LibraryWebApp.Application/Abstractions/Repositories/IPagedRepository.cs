namespace LibraryWebApp.Application.Abstractions.Repositories
{
    public interface IPagedRepository<T>
    {
        public Task<PagedResult<T>> GetPaged(PaginationParams paginationParams);
    }
}
