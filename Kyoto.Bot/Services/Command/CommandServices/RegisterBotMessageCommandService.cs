using Kyoto.Bot.Services.Bot;
using Kyoto.Domain.Bot;
using Kyoto.Domain.Command;
using Kyoto.Domain.System;

namespace Kyoto.Bot.Services.Command.CommandServices;

public class RegisterBotMessageCommandService : IMessageCommandService
{
    private readonly IBotService _botService;
    private readonly BotPostService _botPostService;
    private readonly IExecutiveCommandRepository _executiveCommandRepository;
    
    public ExecutiveCommandType ExecutiveCommandType => ExecutiveCommandType.RegisterBot;

    public RegisterBotMessageCommandService(
        IBotService botService, 
        BotPostService botPostService,
        IExecutiveCommandRepository executiveCommandRepository)
    {
        _botService = botService;
        _botPostService = botPostService;
        _executiveCommandRepository = executiveCommandRepository;
    }

    public async Task ExecuteAsync(Session session, MessageCommandData messageCommandData)
    {
        var botId = await _botService.SaveAsync(session, messageCommandData.Message.Text!);
        await _botPostService.SendMessageSuccessfulRegistrationAsync(session);
        await _executiveCommandRepository.SaveExecutiveCommandAsync(session, ExecutiveCommandType.UpdateBotName, botId);
    }
}