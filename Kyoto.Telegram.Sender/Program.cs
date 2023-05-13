using Confluent.Kafka;
using Kyoto.DI;
using Kyoto.Kafka;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Handlers;
using Kyoto.Kafka.Interfaces;
using Kyoto.Logger;
using Kyoto.Services.Tenant;
using Kyoto.Settings;
using Kyoto.Telegram.Sender;
using Kyoto.Telegram.Sender.Interfaces;
using Kyoto.Telegram.Sender.KafkaHandlers;
using Kyoto.Telegram.Sender.Services;
using TBot.Client.AspNet;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSettings<KafkaSettings>(builder.Configuration, out var kafkaSettings);

builder.Services.AddTransient<IRequestService, RequestService>();
builder.Services.AddKafkaProducer<string>(new ProducerConfig { BootstrapServers = kafkaSettings.BootstrapServers });
builder.Services.AddKafkaConsumersFactory();
builder.Services.AddTransient<IKafkaEventSubscriber, KafkaEventSubscriber>();
builder.Logging.AddLogger(builder.Configuration, kafkaSettings);

builder.Services.AddTelegramTBot();
builder.Services.AddTBotRedisLimiter("localhost,password=password,defaultDatabase=0");

var app = builder.Build();

var kafkaConsumerFactory = app.Services.GetRequiredService<IKafkaConsumerFactory>();
var consumerConfig = new ConsumerConfig{BootstrapServers = kafkaSettings.BootstrapServers};
await kafkaConsumerFactory.SubscribeAsync<InitTenantEvent, InitSenderTenantHandler>(consumerConfig);

var kafkaProducer = app.Services.GetRequiredService<IKafkaProducer<string>>();
await kafkaProducer.ProduceAsync(new RequestTenantEvent { SessionId = Guid.NewGuid() }, string.Empty);

app.MapGet("/", () => "Kyoto.Telegram.Sender 0.1");
await app.RunAsync();