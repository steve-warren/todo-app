namespace WarrenSoftware.TodoApp.Core.Domain;

public record AuthenticationResult : Enumeration
{
    public static AuthenticationResult Parse(string name)
    {
        return name switch
        {
            nameof(Success) => Success,
            nameof(InvalidUserNameAndOrPassword) => InvalidUserNameAndOrPassword,
            _ => throw new ArgumentException("invalid name", nameof(name))
        };
    }

    public static readonly AuthenticationResult Success = new() { Name = nameof(Success) };
    public static readonly AuthenticationResult InvalidUserNameAndOrPassword = new() { Name = nameof(InvalidUserNameAndOrPassword) };
}
