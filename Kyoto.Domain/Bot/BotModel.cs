namespace Kyoto.Domain.Bot;

public class BotModel
{
    public Guid Id { get; set; }
    public string PrivateId { get; private set; }
    public string FirstName { get; private set; }
    public string Username { get; private set; }
    public bool CanJoinGroups { get; private set; }
    public bool CanReadAllGroupMessages { get; private set; }
    public bool SupportsInlineQueries { get; private set; }
    public string Token { get; private set; }
    public bool IsEnable { get; private set; }

    public BotModel(
        Guid id,
        string privateId,
        string firstName, 
        string username,
        bool canJoinGroups, 
        bool canReadAllGroupMessages, 
        bool supportsInlineQueries, 
        string token, 
        bool isEnable)
    {
        Id = id;
        PrivateId = privateId;
        FirstName = firstName;
        Username = username;
        CanJoinGroups = canJoinGroups;
        CanReadAllGroupMessages = canReadAllGroupMessages;
        SupportsInlineQueries = supportsInlineQueries;
        Token = token;
        IsEnable = isEnable;
    }

    public static BotModel Create(
        string privateId,
        string firstName, 
        string username,
        bool canJoinGroups, 
        bool canReadAllGroupMessages, 
        bool supportsInlineQueries, 
        string token, 
        bool isEnable)
    {
        return new BotModel(
            Guid.NewGuid(), 
            privateId, 
            firstName, 
            username, 
            canJoinGroups,
            canReadAllGroupMessages,
            supportsInlineQueries, 
            token, 
            isEnable);
    }
    
    public static BotModel CreateWithOnlyToken(string token)
    {
        return new BotModel(
            Guid.NewGuid(), 
            string.Empty, 
            string.Empty, 
            string.Empty, 
            false,
            false,
            false, 
            token, 
            false);
    }

    public BotModel Init(
        string privateId,
        string firstName, 
        string username,
        bool canJoinGroups, 
        bool canReadAllGroupMessages, 
        bool supportsInlineQueries)
    {
        PrivateId = privateId;
        FirstName = firstName;
        Username = username;
        CanJoinGroups = canJoinGroups;
        CanReadAllGroupMessages = canReadAllGroupMessages;
        SupportsInlineQueries = supportsInlineQueries;
        return this;
    }
}