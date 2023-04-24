namespace Kyoto.Domain.System;

public class Session
{
    public Guid Id { get; }
    public long ChatId { get; }
    public long ExternalUserId { get; }

    private Session(Guid id, long chatId, long externalUserId)
    {
        Id = id;
        ChatId = chatId;
        ExternalUserId = externalUserId;
    }

    public static Session Create(Guid id, long chatId, long externalUserId)
    {
        return new Session(id, chatId, externalUserId);
    }
}