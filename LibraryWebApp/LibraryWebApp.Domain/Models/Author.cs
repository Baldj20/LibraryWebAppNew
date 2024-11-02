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
        public void AddBook(Book book)
        {
            Books.Add(book);
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Country { get; set; }
        public List<Book> Books { get; set; }
    }
}
