using Kyoto.Domain.Command;

namespace Kyoto.Domain.Telegram.Types;

public class Message
{
    public int MessageId { get; set; }
    public User? FromUser { get; set; }
    public Chat Chat { get; set; } = null!;
    public string? Text { get; set; }
    public List<MessageEntity>? MessageEntities { get; set; }
    public Contact? Contact { get; set; }

    public bool TryGetCommand(out CommandType? command)
    {
        command = null;
        if (MessageEntities is null || Text is null)
        {
            return false;
        }

        var commandEntity = MessageEntities.FirstOrDefault(
            x => x.Type.Value == MessageEntityType.MessageEntityTypeValue.BotCommand);

        if (commandEntity is null)
        {
            return false;
        }

        command = CommandTypeExtension.Get(Text.Substring(commandEntity.Offset, commandEntity.Length));
        return true;
    }
}