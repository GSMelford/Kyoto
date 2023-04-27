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

    public async Task SaveAsync(
        Session session,
        CommandType command, 
        object? additionalData = null)
    {
        var executiveTelegramCommandDal = await GetAsync(session.ExternalUserId);
        if (executiveTelegramCommandDal is not null)
        {
            return;
        }
        
        var executiveTelegramCommand = new Models.ExecutiveCommand
        {
            SessionId = session.Id,
            ExternalUserId = session.ExternalUserId,
            ChatId = session.ChatId,
            Command = command.ToString(),
            AdditionalData = additionalData?.ToString(),
            StepState = (int)ExecutiveCommandStep.FirstStep,
            Step = (int)CommandStepState.RequestToAction
        };

        await _databaseContext.AddAsync(executiveTelegramCommand);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Session session, ExecutiveCommand executiveCommand)
    {
        var executiveCommandDal = await GetAsync(session.ExternalUserId);

        executiveCommandDal!.Step = (int)executiveCommand.Step;
        executiveCommandDal.StepState = (int)executiveCommand.StepState;
        executiveCommandDal.AdditionalData = executiveCommand.AdditionalData;
        
        _databaseContext.Update(executiveCommandDal);
        await _databaseContext.SaveChangesAsync();
    }
    
    public async Task<bool> IsExistAsync(Session session)
    {
        return await GetAsync(session.ExternalUserId) is not null;
    }
    
    public async Task RemoveAsync(Session session)
    {
        var executiveCommand = await GetAsync(session.ExternalUserId);
        _databaseContext.Remove(executiveCommand!);
        await _databaseContext.SaveChangesAsync();
    }
    
    public async Task<ExecutiveCommand> GetAsync(Session session)
    {
        return (await GetAsync(session.ExternalUserId))!.ToDomain();
    }
    
    private async Task<Models.ExecutiveCommand?> GetAsync(long externalUserId)
    {
        return await _databaseContext.Set<Models.ExecutiveCommand>()
            .FirstOrDefaultAsync(x => x.ExternalUserId == externalUserId);
    }
}