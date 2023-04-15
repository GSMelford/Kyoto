using Kyoto.Domain.ExecutiveCommand;
using Microsoft.EntityFrameworkCore;
using ExecutiveTelegramCommand = Kyoto.Domain.ExecutiveCommand.ExecutiveTelegramCommand;

namespace Kyoto.Infrastructure.Repositories.ExecutiveCommand;

public class ExecutiveTelegramCommandRepository : IExecutiveTelegramCommandRepository
{
    private readonly IDatabaseContext _databaseContext;

    public ExecutiveTelegramCommandRepository(IDatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<bool> SaveExecutiveCommandAsync(long telegramId, ExecutiveCommandType executiveCommand)
    {
        var executiveTelegramCommandDal =
           await _databaseContext.Set<Models.ExecutiveTelegramCommand>()
               .FirstOrDefaultAsync(x => x.TelegramId == telegramId);

        if (executiveTelegramCommandDal is not null) {
            return false;
        }
        
        var executiveTelegramCommand = new Models.ExecutiveTelegramCommand
        {
            TelegramId = telegramId,
            Command = executiveCommand.ToString()
        };

        await _databaseContext.AddAsync(executiveTelegramCommand);
        await _databaseContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IsExecutiveCommandExistAsync(long telegramId)
    {
        return await _databaseContext.Set<Models.ExecutiveTelegramCommand>()
            .FirstOrDefaultAsync(x => x.TelegramId == telegramId) is not null;
    }
    
    public async Task<ExecutiveTelegramCommand> PopExecutiveCommandAsync(long telegramId)
    {
        var command = await _databaseContext.Set<Models.ExecutiveTelegramCommand>()
            .FirstAsync(x => x.TelegramId == telegramId);
        
        _databaseContext.Remove(command);
        await _databaseContext.SaveChangesAsync();

        return command.ToDomain();
    }
}