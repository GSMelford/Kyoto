namespace Kyoto.Domain.PreparedMessagesSystem;

public interface IPreparedMessagesRepository
{
    Task<Guid> GetPostEventIdAsync(string postEventName);
    Task AddNewsletterAsync(PreparedMessage preparedMessage);
    Task<List<PostEvent>> GetEventsAsync();
}