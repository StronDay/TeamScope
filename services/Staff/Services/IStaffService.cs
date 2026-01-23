namespace Staff.Services;

public interface IStaffService
{
    Task CreateAsync(string firstName, string secondName, CancellationToken cancellationToken = default);
}
