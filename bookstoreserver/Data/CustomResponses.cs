namespace bookstoreserver.Data
{
    public class CustomResponses
    {
        public static readonly string LoginError = "Username or password not correct.";
        public static readonly string UserFound = "User with the same username already exists.";
        public static readonly string OnCreate = "Book with that name already exists.";
        public static readonly string OnUpdate = "Book with that name does not exist.";
        public static readonly string OnDelete = "Book with that name does not exist.";
        public static readonly string UserNotFound = "User not found.";
    }
}
