namespace Kyoto.Domain.System;

public class Session
{
    public Guid Id { get; private set; }
    public long ChatId { get; private set; }
    public long ExternalUserId { get; private set; }
    public int MessageId { get; private set; }
    public string TenantKey { get; set; }

    private Session(Guid id, long chatId, long externalUserId, int messageId, string tenantKey)
    {
        Id = id;
        ChatId = chatId;
        ExternalUserId = externalUserId;
        MessageId = messageId;
        TenantKey = tenantKey;
    }

    public static Session Create(Guid id, long chatId, long externalUserId, int messageId, string tenantKey)
    {
        return new Session(id, chatId, externalUserId, messageId, tenantKey);
    }
}