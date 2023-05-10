using Kyoto.Domain.Telegram.Types;
using User = Kyoto.Domain.BotFactory.Authorization.User;

namespace Kyoto.Commands;

public static class Converter
{
    public static User ToUserDomain(this Message message)
    {
        return User.Create(
            message.Contact!.FirstName,
            message.Contact.LastName, 
            message.FromUser!.Id,
            message.FromUser!.Username!, 
            message.Contact.PhoneNumber);
    }
}