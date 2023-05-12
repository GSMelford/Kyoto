using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.CommandSystem.Interfaces;
using Kyoto.Domain.System;
using Microsoft.EntityFrameworkCore;

namespace Kyoto.Database.CommonRepositories.CommandSystem;

public class CommandRepository : ICommandRepository
{
    private readonly IDatabaseContext _databaseContext;

    public CommandRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<bool> TrySaveCommandAsync(Session session, string commandName)
    {
        var commandDal = await GetAsync(session.ExternalUserId);
        
        if (commandDal is not null)
            return false;
        
        commandDal = new CommonModels.Command
        {
            SessionId = session.Id,
            ExternalUserId = session.ExternalUserId,
            ChatId = session.ChatId,
            Name = commandName,
            State = 0,
            Step = 0
        };

        await _databaseContext.AddAsync(commandDal);
        await _databaseContext.SaveChangesAsync();
        return true;
    }

    public async Task UpdateCommandAsync(Session session, Command command)
    {
        var commandDal = await GetAsync(session.ExternalUserId);

        commandDal!.Step = (int)command.Step;
        commandDal.State = (int)command.State;
        commandDal.AdditionalData = command.AdditionalData;
        
        _databaseContext.Update(commandDal);
        await _databaseContext.SaveChangesAsync();
    }
    
    public async Task<bool> IsCommandExistsAsync(Session session)
    {
        return await GetAsync(session.ExternalUserId) is not null;
    }
    
    public async Task RemoveAsync(Session session)
    {
        var executiveCommand = await GetAsync(session.ExternalUserId);
        _databaseContext.Remove(executiveCommand!);
        await _databaseContext.SaveChangesAsync();
    }
    
    public async Task<Command> GetAsync(Session session)
    {
        return (await GetAsync(session.ExternalUserId))!.ToDomain();
    }
    
    private async Task<CommonModels.Command?> GetAsync(long externalUserId)
    {
        return await _databaseContext.Set<CommonModels.Command>()
            .FirstOrDefaultAsync(x => x.ExternalUserId == externalUserId);
    }
}