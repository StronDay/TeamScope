using Auth.Core.App.Abstractions;
using Auth.Core.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Npgsql.Internal;

namespace Auth.Core.App.Services;

public class AccountService(IAccountRepository accountRepository) : IAccountService
{
    public async Task CreateAsync(string userName, string password, CancellationToken cancellationToken = default)
    {
        var accountModel = new AccountModel
        {
            UserName = userName,
            Id = Guid.NewGuid()
        };

        var passwordHash = new PasswordHasher<AccountModel>().HashPassword(accountModel, password); 

        accountModel.PasswordHash = passwordHash; 

        await accountRepository.CreateAsync(accountModel, cancellationToken);
    }

    public async Task<AccountModel?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return await accountRepository.GetByUserNameAsync(userName, cancellationToken);
    }
}
