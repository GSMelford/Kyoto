using Kyoto.Domain.Command;
using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;

namespace Kyoto.Bot.Services.Command.ExecutiveCommandSystem;

public class ExecutiveCommandService : IExecutiveCommandService
{
    private readonly IExecutiveCommandRepository _executiveCommandRepository;
    private readonly IExecutiveCommandFactory _executiveCommandFactory;

    public ExecutiveCommandService(
        IExecutiveCommandRepository executiveCommandRepository, 
        IExecutiveCommandFactory executiveCommandFactory)
    {
        _executiveCommandRepository = executiveCommandRepository;
        _executiveCommandFactory = executiveCommandFactory;
    }
    
    public async Task HandleExecutiveCommandIfExistAsync(Session session, Message message)
    {
        if (await _executiveCommandRepository.IsExecutiveCommandExistAsync(session))
        {
            var command = await _executiveCommandRepository.PopExecutiveCommandAsync(session);
            var commandService = _executiveCommandFactory.CreateMessageCommandService(command.ExecutiveCommandValue);
            await commandService.ExecuteAsync(session, MessageCommandData.Create(message, command.AdditionalData));
        }
    }
    
    public async Task HandleExecutiveCommandIfExistAsync(Session session, CallbackQuery callbackQuery)
    {
        if (await _executiveCommandRepository.IsExecutiveCommandExistAsync(session))
        {
            var command = await _executiveCommandRepository.PopExecutiveCommandAsync(session);
            var commandService = _executiveCommandFactory.CreateCallbackQueryCommandService(command.ExecutiveCommandValue);
            await commandService.ExecuteAsync(session, CallbackQueryCommandData.Create(callbackQuery, command.AdditionalData));
        }
    }
}