namespace Kyoto.Database;

public interface IDatabaseFactory
{
    IDatabaseContext GetContext(string tenantKey);
}