namespace Auth.Core.Domain.Models;

public class AccountModel
{
    public Guid Id { get; set; }
    public string? UserName { get; set; }
    public string? PasswordHash { get; set; }
}
