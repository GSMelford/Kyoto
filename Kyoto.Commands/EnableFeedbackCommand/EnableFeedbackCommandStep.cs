using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.Menu;
using Kyoto.Domain.Menu.Interfaces;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.Processors;
using Kyoto.Domain.RequestSystem;
using Kyoto.Extensions;
using Kyoto.Services.CommandSystem;
using Kyoto.Settings;

namespace Kyoto.Commands.EnableFeedbackCommand;

public class EnableFeedbackCommandStep : BaseCommandStep
{
    private readonly IPostService _postService;
    private readonly IRequestService _requestService;
    private readonly KyotoBotFactorySettings _kyotoBotFactorySettings;
    private readonly IMenuRepository _menuRepository;
    private readonly IMenuService _menuService;

    public EnableFeedbackCommandStep(
        IPostService postService, 
        IRequestService requestService,
        KyotoBotFactorySettings kyotoBotFactorySettings, 
        IMenuRepository menuRepository, 
        IMenuService menuService)
    {
        _postService = postService;
        _requestService = requestService;
        _kyotoBotFactorySettings = kyotoBotFactorySettings;
        _menuRepository = menuRepository;
        _menuService = menuService;
    }

    protected override async Task<CommandStepResult> SetActionRequestAsync()
    {
        await _postService.SendConfirmationMessageAsync(Session, "🤔 Ви дійсно хочите увімкнути збір відгуків?");
        return CommandStepResult.CreateSuccessful();
    }

    protected override async Task<CommandStepResult> SetProcessResponseAsync()
    {
        if (CommandContext.CallbackQuery!.Data == CallbackQueryButtons.Confirmation)
        {
            await _postService.DeleteMessageAsync(Session);
            await _postService.SendTextMessageAsync(Session, 
                $"🤔 Ви дійсно хочите увімкнути збір відгуків\\?\nВідповідь: {CallbackQueryButtons.Confirmation}");
            
            var isSuccess = await _requestService.SendWithStatusCodeAsync(
                new RequestCreator(HttpMethod.Post, _kyotoBotFactorySettings.ClientBaseUrl + "/api/feedback")
                .AddTenantHeader(CommandContext.AdditionalData!)
                .AddParameters(new Dictionary<string, string>
                {
                    { "isEnable", bool.TrueString }
                }).Create());

            if (!isSuccess) {
                await _postService.SendTextMessageAsync(Session, "😨 Щось пішло не так\\.");
                return CommandStepResult.CreateInterrupt();
            }

            await _menuRepository.RemoveAccessToWatchButtonAsync(Session.ExternalUserId, MenuPanelConstants.Button.EnableCollectFeedback);
            await _menuRepository.AddAccessToWatchButtonAsync(Session.ExternalUserId, MenuPanelConstants.Button.DisableCollectFeedback);
            
            await _postService.SendTextMessageAsync(Session, "🎉 Збір відгуків увімкнено!\\!");
            await _menuService.SendMenuIfExistsAsync(Session, MenuPanelConstants.BotFeaturesMenuPanel);
            return CommandStepResult.CreateSuccessful();
        }
        
        await _postService.SendTextMessageAsync(Session, 
            $"🤔 Ви дійсно хочите увімкнути збір відгуків\\?\nВідповідь: {CallbackQueryButtons.Cancel}");
        
        return CommandStepResult.CreateSuccessful();
    }
}