using Kyoto.Domain.PreparedMessagesSystem;

namespace Kyoto.Database.CommonRepositories.PreparedMessage;

public static class Converter
{
    public static PostEvent ToDomain(this CommonModels.PostEvent postEvent)
    {
        return PostEvent.Create(postEvent.Name, postEvent.Code);
    }
}