namespace api.Models;

public class Employee
{
    public long Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? JobTitle { get; set; }
    public string? Patronymic {get; set; }
    public string? Age { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}