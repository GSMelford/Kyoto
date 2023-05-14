using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.TemplateMessage;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Kyoto.Services.CommandSystem;

namespace Kyoto.Commands.BotFactory.SetRegistrationCommand;

public class ChangeHelloMessageCommandStep : BaseCommandStep
{
    private readonly IPostService _postService;
    private readonly IKafkaProducer<string> _kafkaProducer;

    public ChangeHelloMessageCommandStep(IPostService postService, IKafkaProducer<string> kafkaProducer)
    {
        _postService = postService;
        _kafkaProducer = kafkaProducer;
    }

    protected override async Task<CommandStepResult> SetActionRequestAsync()
    {
        await _postService.SendTextMessageAsync(Session, "We are changing the welcome message!");
        await _kafkaProducer.ProduceAsync(new TemplateMessageEvent
        {
            Action = TemplateMessageEventAction.Send,
            Type = TemplateMessageTypeValue.StartMessage
        }, CommandContext.AdditionalData!);

        return CommandStepResult.CreateSuccessful();
    }

    protected override async Task<CommandStepResult> SetProcessResponseAsync()
    {
        var newText = CommandContext.Message!.Text!;
        await _postService.SendTextMessageAsync(Session, "Your new message looks like this: ");
        await _postService.SendTextMessageAsync(Session, newText);
        
        await _kafkaProducer.ProduceAsync(new TemplateMessageEvent
        {
            Action = TemplateMessageEventAction.Update,
            Type = TemplateMessageTypeValue.StartMessage,
            Text = newText
        }, CommandContext.AdditionalData!);
        
        return CommandStepResult.CreateSuccessful();
    }
}