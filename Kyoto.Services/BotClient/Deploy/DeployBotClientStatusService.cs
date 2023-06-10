using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.System;
using Kyoto.Domain.Tenant.Interfaces;
using Kyoto.Services.BotFactory.PostSystem;
using TBot.Client.Parameters;
using TBot.Client.Requests;

namespace Kyoto.Services.BotClient.Deploy;

public class DeployBotClientStatusService : IDeployStatusService
{
    private readonly IPostService _postService;

    public DeployBotClientStatusService(IPostService postService)
    {
        _postService = postService;
    }

    public async Task Notify(Session session)
    {
        var newSession = Session.CreatePersonalNew(session.TenantKey, session.ExternalUserId);
        await _postService.PostAsync(newSession, new SendMessageRequest(new SendMessageParameters
        {
            Text = "Я успішно активований! 👨‍💻",
            ChatId = session.ChatId
        }).ToRequest());
        
        await _postService.PostAsync(newSession, new SendMessageRequest(new SendMessageParameters
        {
            Text = "Ви можете налаштувати мої функції в *Kyoto Bot Factory* ⚙️",
            ChatId = session.ChatId,
            ParseMode = ParseMode.MarkdownV2
        }).ToRequest());

        await _postService.SendStickerMessageAsync(session, "CAACAgIAAxUAAWSEa3NLOXR0rnIWNh6olo3v1LY2AAIzBwACRvusBB9PEXZlMCInLwQ");
    }
}