namespace Kyoto.Domain.System;

public class Session
{
    public Guid Id { get; private set; }
    public long ChatId { get; private set; }
    public long ExternalUserId { get; private set; }
    public int MessageId { get; private set; }

    private Session(Guid id, long chatId, long externalUserId, int messageId)
    {
        Id = id;
        ChatId = chatId;
        ExternalUserId = externalUserId;
        MessageId = messageId;
    }

    public static Session Create(Guid id, long chatId, long externalUserId, int messageId)
    {
        return new Session(id, chatId, externalUserId, messageId);
    }
}