using Kyoto.Domain.Tenant;

namespace Kyoto.Bot.Client.Middlewares;

public class TenantIdentifierMiddleware
{
    private const string TenantKey = "Tenant";
    private readonly RequestDelegate _next;
    
    public TenantIdentifierMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.Headers.TryGetValue(TenantKey, out var tenantKey);
        if (string.IsNullOrEmpty(tenantKey)) 
        {
            await _next(context);
            return;
        }

        using (CurrentBotTenant.SetBotTenant(BotTenantModel.Create(tenantKey!)))
        {
            await _next(context);
        }
    }
}