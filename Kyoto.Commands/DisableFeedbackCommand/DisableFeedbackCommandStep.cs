using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.Menu;
using Kyoto.Domain.Menu.Interfaces;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.Processors;
using Kyoto.Domain.RequestSystem;
using Kyoto.Extensions;
using Kyoto.Services.CommandSystem;
using Kyoto.Settings;

namespace Kyoto.Commands.DisableFeedbackCommand;

public class DisableFeedbackCommandStep : BaseCommandStep
{
    private readonly IPostService _postService;
    private readonly IRequestService _requestService;
    private readonly KyotoBotFactorySettings _kyotoBotFactorySettings;
    private readonly IMenuRepository _menuRepository;
    private readonly IMenuService _menuService;

    public DisableFeedbackCommandStep(
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
        await _postService.SendConfirmationMessageAsync(Session, "🤔 Ви дійсно хочите вимкнути збір відгуків?");
        return CommandStepResult.CreateSuccessful();
    }

    protected override async Task<CommandStepResult> SetProcessResponseAsync()
    {
        if (CommandContext.CallbackQuery!.Data == CallbackQueryButtons.Confirmation)
        {
            await _postService.DeleteMessageAsync(Session);
            await _postService.SendTextMessageAsync(Session, 
                $"🤔 Ви дійсно хочите вимкнути збір відгуків\\?\nВідповідь: {CallbackQueryButtons.Confirmation}");
            
            var isSuccess = await _requestService.SendWithStatusCodeAsync(
                new RequestCreator(HttpMethod.Patch, _kyotoBotFactorySettings.ClientBaseUrl + "/api/feedback")
                    .AddTenantHeader(CommandContext.AdditionalData!)
                    .AddParameters(new Dictionary<string, string>
                    {
                        { "isEnable", bool.FalseString }
                    }).Create());

            if (!isSuccess) {
                await _postService.SendTextMessageAsync(Session, "😨 Щось пішло не так\\.");
                return CommandStepResult.CreateInterrupt();
            }
        
            await _menuRepository.SetMenuButtonStatusAsync(MenuPanelConstants.Button.EnableCollectFeedback);
            await _menuRepository.SetMenuButtonStatusAsync(MenuPanelConstants.Button.DisableCollectFeedback, false);
            
            await _postService.SendTextMessageAsync(Session, "🎉 Збір відгуків вимкнуто!\\!");
            await _menuService.SendMenuIfExistsAsync(Session, MenuPanelConstants.BotFeaturesMenuPanel);
            return CommandStepResult.CreateSuccessful();
        }
        
        await _postService.SendTextMessageAsync(Session, 
            $"🤔 Ви дійсно хочите вимкнути збір відгуків\\?\nВідповідь: {CallbackQueryButtons.Cancel}");
        
        return CommandStepResult.CreateSuccessful();
    }
}