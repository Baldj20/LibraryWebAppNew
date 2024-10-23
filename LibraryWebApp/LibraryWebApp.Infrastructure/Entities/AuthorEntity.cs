namespace LibraryWebApp.Infrastructure.Entities
{
    public class AuthorEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public List<BookEntity> Books { get; set; }
    }
}
