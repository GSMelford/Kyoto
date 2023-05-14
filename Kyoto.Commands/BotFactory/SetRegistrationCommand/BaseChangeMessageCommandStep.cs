using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.TemplateMessage;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.BotFactory.SetRegistrationCommand;

public abstract class BaseChangeMessageCommandStep : BaseCommandStep
{
    private readonly IPostService _postService;
    private readonly IKafkaProducer<string> _kafkaProducer;
    protected abstract TemplateMessageTypeValue TemplateMessageType { get; }
    protected virtual string AdditionalText { get; } = string.Empty;
    
    public BaseChangeMessageCommandStep(IPostService postService, IKafkaProducer<string> kafkaProducer)
    {
        _postService = postService;
        _kafkaProducer = kafkaProducer;
    }

    protected override async Task<CommandStepResult> SetActionRequestAsync()
    {
        string tenantKey = CommandContext.AdditionalData!;
        await _kafkaProducer.ProduceAsync(new TemplateMessageEvent (Domain.System.Session.CreatePersonalNew(tenantKey, Session.ChatId))
        {
            Action = TemplateMessageEventAction.Send,
            Type = TemplateMessageType
        }, tenantKey);
        
        await _postService.SendTextMessageAsync(Session, $"{AdditionalText}✍️ Enter new text:");
        return CommandStepResult.CreateSuccessful();
    }

    protected override async Task<CommandStepResult> SetProcessResponseAsync()
    {
        string tenantKey = CommandContext.AdditionalData!;
        var newText = CommandContext.Message!.Text!;
        await _postService.SendTextMessageAsync(Session, "⏩ Your new text looks like this: ");
        await _postService.SendTextMessageAsync(Session, newText);
        
        await _kafkaProducer.ProduceAsync(new TemplateMessageEvent (Domain.System.Session.CreatePersonalNew(tenantKey, Session.ChatId))
        {
            Action = TemplateMessageEventAction.Update,
            Type = TemplateMessageType,
            Text = newText
        }, CommandContext.AdditionalData!);
        
        return CommandStepResult.CreateSuccessful();
    }
}