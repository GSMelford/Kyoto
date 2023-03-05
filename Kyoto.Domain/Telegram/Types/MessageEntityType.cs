namespace Kyoto.Domain.Telegram.Types;

public class MessageEntityType
{
    public MessageEntityTypeValue Value { get; set; }
    
    public MessageEntityType(MessageEntityTypeValue value)
    {
        Value = value;
    }
    
    public static implicit operator MessageEntityType(string value)
    {
        return new MessageEntityType(value switch
        {
            "mention" => MessageEntityTypeValue.Mention,
            "hashtag" => MessageEntityTypeValue.Hashtag,
            "cashtag" => MessageEntityTypeValue.CashTag,
            "bot_command" => MessageEntityTypeValue.BotCommand,
            "url" => MessageEntityTypeValue.Url,
            "email" => MessageEntityTypeValue.Email,
            "phone_number" => MessageEntityTypeValue.PhoneNumber,
            "bold" => MessageEntityTypeValue.Bold,
            "italic" => MessageEntityTypeValue.Italic,
            "underline" => MessageEntityTypeValue.Underline,
            "strikethrough" => MessageEntityTypeValue.Strikethrough,
            "spoiler" => MessageEntityTypeValue.Spoiler,
            "code" => MessageEntityTypeValue.Code,
            "pre" => MessageEntityTypeValue.Pre,
            "text_link" => MessageEntityTypeValue.TextLink,
            "text_mention" => MessageEntityTypeValue.TextMention,
            "custom_emoji" => MessageEntityTypeValue.CustomEmoji,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
        });
    }
    
    public enum MessageEntityTypeValue
    {
        Mention,
        Hashtag,
        CashTag,
        BotCommand,
        Url,
        Email,
        PhoneNumber,
        Bold,
        Italic,
        Underline,
        Strikethrough,
        Spoiler,
        Code,
        Pre,
        TextLink,
        TextMention,
        CustomEmoji
    }
}