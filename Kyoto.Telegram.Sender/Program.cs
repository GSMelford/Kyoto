using Confluent.Kafka;
using Kyoto.Domain.Tenant;
using Kyoto.Kafka;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Kyoto.Telegram.Sender;
using Kyoto.Telegram.Sender.Interfaces;
using Kyoto.Telegram.Sender.KafkaHandlers;
using Kyoto.Telegram.Sender.Services;
using TBot.Client;
using TBot.Client.AspNet;
using TBot.Core.RequestLimiter;

var builder = WebApplication.CreateBuilder(args);

AppSettings appSettings = new AppSettings();
builder.Configuration.Bind(nameof(AppSettings), appSettings);
builder.Services.AddSingleton(appSettings);
builder.Services.AddTransient<IRequestService, RequestService>();
builder.Services.AddKafkaProducer<string>(new ProducerConfig { BootstrapServers = appSettings.KafkaBootstrapServers });
builder.Services.AddKafkaConsumersFactory();

builder.Services.AddTelegramTBot(botBuilder =>
    botBuilder
        .AddBotSettings(new BotSettings(BotTenantFactory.Store.Get(CurrentBotTenant.BotTenant?.TenantKey!).Token))
        .AddLimitConfig(new LimitConfig { StoreName = CurrentBotTenant.BotTenant?.TenantKey! })
        .AddRedisConfig("localhost,password=password,defaultDatabase=0"));

var app = builder.Build();

var kafkaConsumerFactory = app.Services.GetRequiredService<IKafkaConsumerFactory>();
var consumerConfig = new ConsumerConfig{BootstrapServers = appSettings.KafkaBootstrapServers};
kafkaConsumerFactory.Subscribe<RequestEvent, RequestHandler>(consumerConfig);
kafkaConsumerFactory.Subscribe<InitTenantEvent, InitTenantHandler>(consumerConfig, groupId: $"{nameof(InitTenantHandler)}-Sender");

var kafkaProducer = app.Services.GetRequiredService<IKafkaProducer<string>>();
await kafkaProducer.ProduceAsync(new RequestTenantEvent { SessionId = Guid.NewGuid() });

app.MapGet("/", () => "Kyoto.Telegram.Sender 0.1");
await app.RunAsync();