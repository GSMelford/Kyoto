using Kyoto.Domain.Tenant;
using Microsoft.EntityFrameworkCore;

namespace Kyoto.Infrastructure.Repositories.Tenant;

public class TenantRepository : ITenantRepository
{
    private readonly IDatabaseContext _databaseContext;

    public TenantRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public IAsyncEnumerable<BotTenant> GetAllTenantsAsync()
    {
        return _databaseContext.Set<Models.Bot>()
            .Select(x => BotTenant.Create(x.Username, x.Token))
            .AsAsyncEnumerable();
    }
    
    public async Task<BotTenant> GetBotTenantAsync(long externalId, string name)
    {
        var botDal = await _databaseContext.Set<Models.Bot>()
            .FirstAsync(x => x.ExternalUser.PrivateId == externalId && x.Username == name);

        return BotTenant.Create(botDal.Username, botDal.Token);
    }
}