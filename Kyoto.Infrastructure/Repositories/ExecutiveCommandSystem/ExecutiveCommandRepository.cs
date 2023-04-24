using Kyoto.Domain.Command;
using Kyoto.Domain.System;
using Microsoft.EntityFrameworkCore;
using ExecutiveCommand = Kyoto.Domain.Command.ExecutiveCommand;

namespace Kyoto.Infrastructure.Repositories.ExecutiveCommandSystem;

public class ExecutiveCommandRepository : IExecutiveCommandRepository
{
    private readonly IDatabaseContext _databaseContext;

    public ExecutiveCommandRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<bool> SaveExecutiveCommandAsync(
        Session session,
        ExecutiveCommandType executiveCommand, 
        object? additionalData = null)
    {
        var executiveTelegramCommandDal =
           await _databaseContext.Set<Models.ExecutiveCommand>()
               .FirstOrDefaultAsync(x => x.ExternalUserId == session.ExternalUserId);

        if (executiveTelegramCommandDal is not null) {
            return false;
        }
        
        var executiveTelegramCommand = new Models.ExecutiveCommand
        {
            SessionId = session.Id,
            ExternalUserId = session.ExternalUserId,
            ChatId = session.ChatId,
            Command = executiveCommand.ToString(),
            AdditionalData = additionalData?.ToString()
        };

        await _databaseContext.AddAsync(executiveTelegramCommand);
        await _databaseContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IsExecutiveCommandExistAsync(Session session)
    {
        return await _databaseContext.Set<Models.ExecutiveCommand>()
            .FirstOrDefaultAsync(x => x.ExternalUserId == session.ExternalUserId) is not null;
    }
    
    public async Task<ExecutiveCommand> PopExecutiveCommandAsync(Session session)
    {
        var command = await _databaseContext.Set<Models.ExecutiveCommand>()
            .FirstAsync(x => x.ExternalUserId == session.ExternalUserId);
        
        _databaseContext.Remove(command);
        await _databaseContext.SaveChangesAsync();

        return command.ToDomain();
    }
}