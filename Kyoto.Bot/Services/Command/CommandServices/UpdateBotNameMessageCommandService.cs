using Kyoto.Bot.Services.Bot;
using Kyoto.Domain.Bot;
using Kyoto.Domain.Command;
using Kyoto.Domain.Processors;
using Kyoto.Domain.System;

namespace Kyoto.Bot.Services.Command.CommandServices;

public class UpdateBotNameMessageCommandService : IMessageCommandService, ICallbackQueryCommandService
{
    private readonly BotPostService _botPostService;
    private readonly IExecutiveCommandRepository _executiveCommandRepository;
    private readonly IBotService _botService;

    public ExecutiveCommandType ExecutiveCommandType => ExecutiveCommandType.UpdateBotName;

    public UpdateBotNameMessageCommandService(
        BotPostService botPostService,
        IExecutiveCommandRepository executiveCommandRepository,
        IBotService botService)
    {
        _botPostService = botPostService;
        _executiveCommandRepository = executiveCommandRepository;
        _botService = botService;
    }

    public async Task ExecuteAsync(Session session, MessageCommandData messageCommandData)
    {
        await _botPostService.SendUpdateBotNameConfirmationRequestAsync(session, messageCommandData.Message.Text!);
        await _executiveCommandRepository
            .SaveExecutiveCommandAsync(session, ExecutiveCommandType.UpdateBotName, messageCommandData.AdditionalData);
    }

    public async Task ExecuteAsync(Session session, CallbackQueryCommandData callbackQueryCommandData)
    {
        if (callbackQueryCommandData.CallbackQuery.Message!.Text == CallbackQueryButtons.Confirmation)
        {
            await _botService.UpdateBotNameAsync(
                (Guid)callbackQueryCommandData.AdditionalData!,
                callbackQueryCommandData.CallbackQuery.Data!);

            await _botPostService.SendMessageSuccessfulFullyRegistrationAsync(session);
        }
    }
}