namespace LibraryWebApp.Infrastructure.Entities
{
    public class UserBookEntity
    {
        public Guid Id { get; set; }
        public string ISBN { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public UserEntity User { get; set; }
    }
}
