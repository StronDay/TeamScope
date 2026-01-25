using Auth.Core.App.Abstractions;
using Auth.Core.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Npgsql.Internal;

namespace Auth.Core.App.Services;

public class AccountService(IAccountRepository accountRepository, JwtService jwtService) : IAccountService
{
    public async Task RegisterAsync(string userName, string password, CancellationToken cancellationToken = default)
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

    public async Task<string> Login(string userName, string password, CancellationToken cancellationToken = default)
    {
        var account = await accountRepository.GetByUserNameAsync(userName);

        if (account == null)
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        var result = new PasswordHasher<AccountModel>().VerifyHashedPassword
        (
            account, 
            account.PasswordHash,
            password
        );

        if (result == PasswordVerificationResult.Success)
        {
            return jwtService.GenerateToken(account);
        }
        else
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        throw new NotImplementedException();
    }

    public async Task<List<AccountModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await accountRepository.GetAllAsync(cancellationToken);
    }

    public async Task<AccountModel?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return await accountRepository.GetByUserNameAsync(userName, cancellationToken);
    }
}
