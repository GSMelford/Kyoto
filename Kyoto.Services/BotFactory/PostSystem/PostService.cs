using Kyoto.Domain.PostSystem;
using Kyoto.Domain.PostSystem.Interfaces;
using Kyoto.Domain.Processors;
using Kyoto.Domain.System;
using Kyoto.Domain.Tenant;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using TBot.Client.Parameters;
using TBot.Client.Parameters.ReplyMarkupParameters.Buttons;
using TBot.Client.Parameters.ReplyMarkupParameters.Keyboards;
using TBot.Client.Requests;

namespace Kyoto.Services.BotFactory.PostSystem;

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
    
    public Task SendTextMessageAsync(Session session, string text)
    {
        return PostAsync(session, new SendMessageRequest(new SendMessageParameters
        {
            Text = text,
            ChatId = session.ChatId
        }).ToRequest());
    }
    
    public Task SendConfirmationMessageAsync(Session session, string text)
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
        }).ToRequest());
    }
    
    public async Task PostAsync(Session session, Request request)
    {
        await _kafkaProducer.ProduceAsync(new RequestEvent(session)
        {
            Endpoint = request.Endpoint,
            HttpMethod = request.Method,
            Parameters = request.Parameters
        }, session.TenantKey);
    }
    
    public async Task PostBehalfOfFactoryAsync(Session session, Request request)
    {
        await _kafkaProducer.ProduceAsync(new RequestEvent(session)
        {
            Endpoint = request.Endpoint,
            HttpMethod = request.Method,
            Parameters = request.Parameters
        }, BotTenantFactory.Store.GetFactoryTenant());
    }
}