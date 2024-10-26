namespace LibraryWebApp.Infrastructure.Entities
{
    public class BookEntity
    {
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public AuthorEntity Author { get; set; }
        public int Count { get; set; }
    }
}
