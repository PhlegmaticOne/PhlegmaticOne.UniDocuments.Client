namespace UniDocuments.App.Shared.Users;

public class JwtTokenObject
{
    public string? Token { get; init; }
    public int ExpirationInMinutes { get; init; }

    public static JwtTokenObject Empty => new()
    {
        Token = string.Empty,
        ExpirationInMinutes = 1
    };
}