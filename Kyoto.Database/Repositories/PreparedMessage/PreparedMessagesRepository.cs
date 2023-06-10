using Kyoto.Domain.PreparedMessagesSystem;
using Microsoft.EntityFrameworkCore;
using PostEventDal = Kyoto.Database.CommonModels.PostEvent;

namespace Kyoto.Database.Repositories.PreparedMessage;

public class PreparedMessagesRepository : IPreparedMessagesRepository
{
    private readonly IDatabaseContext _databaseContext;

    public PreparedMessagesRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Guid> GetPostEventIdAsync(string postEventName)
    {
       return (await _databaseContext.Set<PostEventDal>().FirstAsync(x => x.Name == postEventName)).Id;
    }
    
    public async Task AddNewsletterAsync(Domain.PreparedMessagesSystem.PreparedMessage preparedMessage)
    {
        await _databaseContext.SaveAsync(new CommonModels.PreparedMessage
        {
            Text = preparedMessage.Text,
            PostEvent = await _databaseContext.Set<PostEventDal>().FirstAsync(x=>x.Code == preparedMessage.PostEventCode),
            TimeToSend = preparedMessage.TimeToSend,
            KeyWords = preparedMessage.KeyWords
        });
    }

    public Task<List<PostEvent>> GetEventsAsync()
    {
        return _databaseContext.Set<PostEventDal>().Select(x => x.ToDomain()).ToListAsync();
    }
}