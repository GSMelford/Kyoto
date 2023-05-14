using Kyoto.Domain.CommandSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.TemplateMessage;
using Kyoto.Kafka.Interfaces;

namespace Kyoto.Commands.BotFactory.SetRegistrationCommand;

public class ChangeRetryRegistrationMessageCommandStep : BaseChangeMessageCommandStep
{
    private readonly IPostService _postService;
    
    protected override TemplateMessageTypeValue TemplateMessageType => TemplateMessageTypeValue.RetryRegistration;
    
    public ChangeRetryRegistrationMessageCommandStep(IPostService postService, IKafkaProducer<string> kafkaProducer) 
        : base(postService, kafkaProducer)
    {
        _postService = postService;
    }

    protected override async Task<CommandStepResult> SetProcessResponseAsync()
    {
        var result = await base.SetProcessResponseAsync();
        await _postService.SendTextMessageAsync(Session, "😊 The command has been changed successfully.");
        return result;
    }
}