using Kyoto.Domain.Telegram.Types;
using Kyoto.Domain.Telegram.Updates;
using TBot.Telegram.Dto.Types;
using TBot.Telegram.Dto.Updates;

namespace Kyoto.Telegram.Receiver.Controllers;

public static class Converter
{
    public static Update ToDomain(this UpdateDto updateDto)
    {
        return new Update
        {
            UpdateId = updateDto.UpdateId,
            Message = updateDto.Message?.ToDomain()
        };
    }

    public static Message ToDomain(this MessageDto messageDto)
    {
        return new Message
        {
            Chat = messageDto.Chat.ToDomain(),
            FromUser = messageDto.From?.ToDomain(),
            MessageId = messageDto.MessageId,
            Text = messageDto.Text,
            MessageEntities = messageDto.Entities?.Select(ToDomain).ToList(),
            Contact = messageDto.Contact?.ToDomain()
        };
    }

    public static Chat ToDomain(this ChatDto chatDto)
    {
        return new Chat
        {
            Id = chatDto.Id,
            Type = chatDto.Type
        };
    }

    public static User ToDomain(this UserDto userDto)
    {
        return new User
        {
            Id = userDto.Id,
            Username = userDto.Username,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName
        };
    }
    
    public static MessageEntity ToDomain(this MessageEntityDto messageEntityDto)
    {
        return new MessageEntity
        {
            Type = messageEntityDto.Type,
            Offset = messageEntityDto.Offset,
            Length = messageEntityDto.Length,
            Language = messageEntityDto.Language,
            Url = messageEntityDto.Url,
            CustomEmojiId = messageEntityDto.CustomEmojiId,
            User = messageEntityDto.User?.ToDomain()
        };
    }
    
    public static Contact ToDomain(this ContactDto contactDto)
    {
        return new Contact
        {
            LastName = contactDto.LastName,
            FirstName = contactDto.FirstName,
            PhoneNumber = contactDto.PhoneNumber,
            Vcard = contactDto.Vcard,
            UserId = contactDto.UserId
        };
    }
}