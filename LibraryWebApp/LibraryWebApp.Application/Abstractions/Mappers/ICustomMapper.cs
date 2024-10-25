namespace LibraryWebApp.Application.Abstractions.Mappers
{
    public interface ICustomMapper<DTO, Entity>
    {
        public Task<Entity> ToEntity(DTO dto);
        public DTO ToDTO(Entity entity);
    }
}
