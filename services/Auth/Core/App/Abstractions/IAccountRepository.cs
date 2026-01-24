using Auth.Core.Domain.Models;

namespace Auth.Core.App.Abstractions;

public interface IAccountRepository
{
    Task CreateAsync(AccountModel accountModel, CancellationToken cancellationToken = default);
    Task<List<AccountModel>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<AccountModel?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
}
