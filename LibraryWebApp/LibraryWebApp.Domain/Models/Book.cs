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
        public string ISBN { get; }
        public string Title { get; }
        public string Genre { get; }
        public string Description { get; }
        public Author Author { get; }
    }
}
