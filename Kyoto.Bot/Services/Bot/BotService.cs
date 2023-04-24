using Kyoto.Domain.Bot;
using Kyoto.Domain.System;

namespace Kyoto.Bot.Services.Bot;

public class BotService : IBotService
{
    private readonly IBotRepository _botRepository;

    public BotService(IBotRepository botRepository)
    {
        _botRepository = botRepository;
    }

    public Task<Guid> SaveAsync(Session session, string token)
    {
        return _botRepository.SaveAsync(session.ExternalUserId, BotModel.Create(token));
    }
    
    public Task UpdateBotNameAsync(Guid botId, string name)
    {
        return _botRepository.UpdateNameAsync(botId, name);
    }
}