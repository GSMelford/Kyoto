namespace Kyoto.Domain.PreparedMessagesSystem;

public interface IPreparedMessagesRepository
{
    Task AddNewsletterAsync(PreparedMessage preparedMessage);
    Task<List<PostEvent>> GetEventsAsync();
    Task<List<PreparedMessage>> GetPreparedMessagesByAnswerAsync();
}