namespace Domain.Constants;

public static class Roles
{
    public const string User = "User";
    public const string Admin = "Admin";
    public const string Creator = "Creator";
    public const string AdminAndCreator = $"{Admin}, {Creator}";
}