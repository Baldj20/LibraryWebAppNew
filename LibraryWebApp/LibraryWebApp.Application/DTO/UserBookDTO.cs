using LibraryWebApp.Domain;

namespace LibraryWebApp.Application.DTO
{
    public class UserBookDTO
    {
        public string ISBN { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string UserLogin { get; set; }
    }
}
