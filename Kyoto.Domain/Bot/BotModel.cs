namespace Kyoto.Domain.Bot;

public class BotModel
{
    public Guid Id { get; set; }
    public string Prefix { get; private set; }
    public string Token { get; private set; }

    private BotModel(Guid id, string prefix, string token)
    {
        Id = id;
        Prefix = prefix;
        Token = token;
    }

    public static BotModel Create(string token)
    {
        var id = Guid.NewGuid();
        return new BotModel(id,id.ToString(), token);
    }
}