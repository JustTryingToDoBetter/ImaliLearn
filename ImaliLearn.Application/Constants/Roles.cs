namespace ImaliLearn.Application.Constants;

public static class Roles
{
    public const string Admin = "Admin";
    public const string User = "User";
    public const string Educator = "Educator";
    
    public static readonly IReadOnlyList<string> All = [Admin, User, Educator];
}
