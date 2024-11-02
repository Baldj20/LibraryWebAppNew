namespace LibraryWebApp.Infrastructure.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() 
            : base("Invalid password")
        {
            
        }
    }
}
