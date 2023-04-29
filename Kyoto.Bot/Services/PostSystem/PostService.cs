using Kyoto.Domain.PostSystem;
using Kyoto.Domain.Processors;
using Kyoto.Domain.System;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Requests;
using ReturnResponseDetails = Kyoto.Domain.PostSystem.ReturnResponseDetails;

namespace Kyoto.Bot.Services.PostSystem;

public class PostService : IPostService
{
    private readonly IKafkaProducer<string> _kafkaProducer;
    
    public PostService(IKafkaProducer<string> kafkaProducer)
    {
        _kafkaProducer = kafkaProducer;
    }

    public Task DeleteMessageAsync(Session session)
    {
        return PostAsync(session, new DeleteMessageRequest(new DeleteMessageParameters
        {
            MessageId = session.MessageId,
            ChatId = session.ChatId
        }).ToRequest());
    }
    
    public Task SendTextMessageAsync(Session session, string text, ReturnResponseDetails? returnResponseDetails = null)
    {
        return PostAsync(session, new SendMessageRequest(new SendMessageParameters
        {
            Text = text,
            ChatId = session.ChatId
        }).ToRequest(), returnResponseDetails);
    }
    
    public Task SendConfirmationMessageAsync(Session session, string text, ReturnResponseDetails? returnResponseDetails = null)
    {
        return PostAsync(session, new SendMessageRequest(new SendMessageParameters
        {
            Text = text,
            ChatId = session.ChatId,
            ReplyMarkup = new InlineKeyboardMarkup()
                .Add(new InlineKeyboardButton
                {
                    Text = CallbackQueryButtons.Confirmation,
                    CallbackData = CallbackQueryButtons.Confirmation
                })
                .Add(new InlineKeyboardButton
                {
                    Text = CallbackQueryButtons.Cancel,
                    CallbackData = CallbackQueryButtons.Cancel
                })
        }).ToRequest(), returnResponseDetails);
    }
    
    public async Task PostAsync(Session session, Request request, ReturnResponseDetails? returnResponseDetails = null)
    {
        await _kafkaProducer.ProduceAsync(new RequestEvent
        { 
            TenantKey = session.TenantKey,
            SessionId = session.Id,
            Endpoint = request.Endpoint,
            HttpMethod = request.Method,
            Headers = request.Headers,
            Parameters = request.Parameters,
            ReturnResponse = returnResponseDetails is null ? null : new ResponseMessageReturn
            {
                HandlerType = returnResponseDetails.HandlerType
            }
        });
    }
}