using Kyoto.Dal.CommonRepositories.ExecuteCommandSystem;
using Kyoto.Domain.BotFactory.Menu;
using Kyoto.Domain.ExecutiveCommand.Interfaces;
using Kyoto.Domain.System;
using Kyoto.Domain.Telegram.Types;
using Kyoto.Services.ExecuteCommand;

namespace Kyoto.Services.BotFactory.ExecutiveCommand;

public class FactoryExecutiveCommandService : BaseExecutiveCommandService
{
    public FactoryExecutiveCommandService(
        IExecutiveCommandRepository executiveCommandRepository, 
        IExecutiveCommandFactory executiveCommandFactory, 
        IServiceProvider serviceProvider) : base(executiveCommandRepository, executiveCommandFactory, serviceProvider)
    {
        
    }

    public override async Task StartExecutiveCommandAsync(Session session, string commandName, object? additionalData = null)
    {
        await ExecutiveCommandRepository.SaveAsync(session, commandName, additionalData);
        await DoExecutiveCommandAsync(session);
    }

    public override async Task ProcessExecutiveCommandIfExistsAsync(Session session, Message? message = null, CallbackQuery? callbackQuery = null)
    {
        var commandName = GetCommand(message?.Text);
        if (!string.IsNullOrEmpty(commandName)) {
            await ExecutiveCommandRepository.SaveAsync(session, commandName);
        }
        
        if (await ExecutiveCommandRepository.IsExistsAsync(session))
        {
            await DoExecutiveCommandAsync(session, message, callbackQuery);
        }
    }

    private static string GetCommand(string? text)
    {
        return text switch
        {
            MenuButtons.BotManagementButtons.RegisterNewBot => nameof(MenuButtons.BotManagementButtons.RegisterNewBot),
            MenuButtons.BotManagementButtons.DeployBot => nameof(MenuButtons.BotManagementButtons.DeployBot),
            MenuButtons.BotManagementButtons.DisableBot => nameof(MenuButtons.BotManagementButtons.DisableBot),
            _ => string.Empty
        };
    }
}