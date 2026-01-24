namespace Staff.Apis;

public class CreateStaffRequest
{
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public string? Patronymic { get; set; }
    public int Age { get; set; }
    public string? JobPosition { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}
