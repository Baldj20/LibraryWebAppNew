namespace LibraryWebApp.Domain
{
    public class Author
    {
        public Author(Guid id, string name, string surname, 
            DateTime birthDate, string country, List<Book> books)
        {
            Id = id;
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
            Country = country;
            Books = books;
        }
        public Guid Id { get; }
        public string Name { get; }
        public string Surname { get; }
        public DateTime BirthDate { get; }
        public string Country { get; }
        public List<Book> Books { get; }
    }
}
