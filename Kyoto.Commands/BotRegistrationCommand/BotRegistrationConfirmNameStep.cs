using Kyoto.Domain.BotFactory.Bot;
using Kyoto.Domain.BotFactory.Bot.Interfaces;
using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.Menu;
using Kyoto.Domain.Menu.Interfaces;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.Processors;
using Kyoto.Extensions;
using Kyoto.Services.BotFactory.PostSystem;
using Kyoto.Services.CommandSystem;
using Kyoto.Services.HttpServices.BotRegistration;
using Newtonsoft.Json;
using TBot.Client.Parameters.Stickers;
using TBot.Client.Requests.Stickers;

namespace Kyoto.Commands.BotRegistrationCommand;

public class BotRegistrationConfirmNameStep : BaseCommandStep
{
    private readonly IPostService _postService;
    private readonly IBotService _botService;
    private readonly IMenuRepository _menuRepository;

    private readonly BotRegistrationHttpService _botRegistrationHttpService;

    private static string BuildConfirmNameQuestion(BotModel botModel) => 
        $"🤔 Ви хочете зареєструвати цього бота?\n\n" +
        $"Ім'я: {botModel.FirstName}\n" +
        $"Псевдонім: {botModel.Username}";

    public BotRegistrationConfirmNameStep(
        IPostService postService, 
        IBotService botService, 
        BotRegistrationHttpService botRegistrationHttpService,
        IMenuRepository menuRepository)
    {
        _postService = postService;
        _botService = botService;
        _botRegistrationHttpService = botRegistrationHttpService;
        _menuRepository = menuRepository;
    }

    protected override async Task<CommandStepResult> SetActionRequestAsync()
    {
        var botModel = CommandContext.AdditionalData!.ToObject<BotModel>();
        botModel = await _botRegistrationHttpService.EnrichBotInfoAsync(botModel);
        await _postService.SendConfirmationMessageAsync(Session, BuildConfirmNameQuestion(botModel));
        CommandContext.SetAdditionalData(botModel.ToJson());
        return CommandStepResult.CreateSuccessful();
    }

    protected override async Task<CommandStepResult> SetProcessResponseAsync()
    {
        var botModel = JsonConvert.DeserializeObject<BotModel>(CommandContext.AdditionalData!)!;
        
        if (CommandContext.CallbackQuery!.Data == CallbackQueryButtons.Confirmation)
        {
            await _postService.DeleteMessageAsync(Session);
            await _postService.SendTextMessageAsync(Session, 
                $"{BuildConfirmNameQuestion(botModel).Replace("_", "\\_")}\nВідповідь: {CallbackQueryButtons.Confirmation}");
            
            await _botService.SaveAsync(Session, botModel);
            await _postService.SendTextMessageAsync(Session, 
                "Бот успішно зареєстрований! 🪄🥰");

            await _postService.PostAsync(Session, new SendStickerRequest(new SendStickersParameters
            {
                ChatId = Session.ChatId,
                Sticker = "CAACAgIAAxUAAWSEa3MKyIkhZRmAabutnAfxiyuFAAJLBwACRvusBJjCZeijaQ8uLwQ"
            }).ToRequest());
            
            await _menuRepository.EnableMenuAsync(MenuPanelConstants.BotFeaturesMenuPanel);
            await _postService.SendTextMessageAsync(Session, 
                "Тепер Вам доступне меню\\: *🪄 Функції бота*");
            
            await _postService.SendTextMessageAsync(Session, 
                $"⚠️ Щоб продовжити, почніть діалог зі своїм ботом [{botModel.FirstName}](http://t.me/{botModel.Username}) " +
                $"та в налаштуваннях бота, виберіть функцію *🚀 Запустити бота*");
            
            return CommandStepResult.CreateSuccessful();
        }

        return CommandStepResult.CreateRetry();
    }

    protected override async Task<CommandStepResult> SetRetryActionRequestAsync()
    {
        var botModel = JsonConvert.DeserializeObject<BotModel>(CommandContext.AdditionalData!)!;
        
        await _postService.DeleteMessageAsync(Session);
        await _postService.SendTextMessageAsync(Session, 
            $"{BuildConfirmNameQuestion(botModel)}\nnВідповідь: {CallbackQueryButtons.Cancel}");
        return CommandStepResult.CreateSuccessful();
    }
}