namespace LibraryWebApp.Domain
{
    public class Book
    {
        public Book
            (string iSBN, string title, string genre, 
            string description, Author author)
        {
            ISBN = iSBN;
            Title = title;
            Genre = genre;
            Description = description;
            Author = author;
        }
        public Book()
        {
            ISBN = string.Empty;
            Title = string.Empty;
            Genre = string.Empty;
            Description = string.Empty;
            Author = new Author();
        }
        public string ISBN { get; }
        public string Title { get; }
        public string Genre { get; }
        public string Description { get; }
        public Author Author { get; }
    }
}
