using Kyoto.Domain.Telegram.Types;
using User = Kyoto.Domain.Authorization.User;

namespace Kyoto.Bot.Services.Command;

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