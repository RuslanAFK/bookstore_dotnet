namespace bookstoreserver.Data
{
    public class CustomResponses
    {
        public static IResult LoginError()
        {
            return Results.Json("Username or password not correct.");
        }
        public static IResult UserFound()
        {
            return Results.Json("User with the same username already exists.");
        }
        public static IResult OnCreate()
        {
            return Results.Json("Book with that name already exists.");
        }
        public static IResult OnUpdate()
        {
            return Results.Json("Book with that name does not exist.");
        }
        public static IResult OnDelete()
        {
            return Results.Json("Book with that name does not exist.");
        }
        public static IResult UserNotFound()
        {
            return Results.Json("User not found.");
        }
    }
}
