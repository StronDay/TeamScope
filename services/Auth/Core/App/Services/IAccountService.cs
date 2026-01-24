using Auth.Core.Domain.Models;

namespace Auth.Core.App.Services;

public interface IAccountService
{
    Task CreateAsync(string userName, string password, CancellationToken cancellationToken = default);
    Task<List<AccountModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<AccountModel?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
}
