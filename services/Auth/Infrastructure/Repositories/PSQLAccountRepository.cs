using Microsoft.EntityFrameworkCore;
using Auth.Core.App.Abstractions;
using Auth.Core.Domain.Models;

namespace Auth.Infrastructure.Repositories;

internal class PSQLAccountRepository(AccountContext context) : IAccountRepository
{
    public async Task CreateAsync(AccountModel accountModel, CancellationToken cancellationToken = default)
    {
        await context.Accounts.AddAsync(accountModel, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<AccountModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Accounts.ToListAsync(cancellationToken);
    }

    public async Task<AccountModel?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
    {
        return await context.Accounts.FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken); 
    }
}
