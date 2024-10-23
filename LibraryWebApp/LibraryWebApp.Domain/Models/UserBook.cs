namespace LibraryWebApp.Domain.Models
{
    public class UserBook
    {
        public string ISBN { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public User User { get; set; }
    }
}
