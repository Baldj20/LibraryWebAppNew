namespace LibraryWebApp.Application.DTO
{
    public class AuthorDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public List<BookDTO> Books { get; set; }

    }
}
