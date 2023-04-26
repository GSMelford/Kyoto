using Kyoto.Domain.PostSystem;
using Kyoto.Domain.Processors;
using Kyoto.Domain.System;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Requests;

namespace Kyoto.Bot.Services.RequestSender;

public class PostService : IPostService
{
    private readonly IKafkaProducer<string> _kafkaProducer;

    public PostService(IKafkaProducer<string> kafkaProducer)
    {
        _kafkaProducer = kafkaProducer;
    }

    public Task SendTextMessageAsync(Session session, string text)
    {
        return SendAsync(session.Id, new SendMessageRequest(new SendMessageParameters
        {
            Text = text,
            ChatId = session.ChatId
        }).ToRequest());
    }
    
    public Task SendConfirmationMessageAsync(Session session, string text, string callbackData)
    {
        return SendAsync(session.Id, new SendMessageRequest(new SendMessageParameters
        {
            Text = text,
            ChatId = session.ChatId,
            ReplyMarkup = new InlineKeyboardMarkup()
                .Add(new InlineKeyboardButton
                {
                    Text = CallbackQueryButtons.Confirmation,
                    CallbackData = callbackData
                })
                .Add(new InlineKeyboardButton
                {
                    Text = CallbackQueryButtons.Cancel,
                    CallbackData = string.Empty
                })
        }).ToRequest());
    }
    
    public async Task SendAsync(Guid sessionId, Request request)
    {
        await _kafkaProducer.ProduceAsync(new RequestEvent
        { 
            SessionId = sessionId,
            Endpoint = request.Endpoint,
            HttpMethod = request.Method,
            Headers = request.Headers,
            Parameters = request.Parameters
        });
    }
}