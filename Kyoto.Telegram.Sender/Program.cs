using Confluent.Kafka;
using Kyoto.Kafka;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Kyoto.Telegram.Sender;
using Kyoto.Telegram.Sender.KafkaHandlers;
using TBot.Client;
using TBot.Client.AspNet;

var builder = WebApplication.CreateBuilder(args);

AppSettings appSettings = new AppSettings();
builder.Configuration.Bind(nameof(AppSettings), appSettings);
builder.Services.AddSingleton(appSettings);
builder.Services.AddKafkaProducer<string>(new ProducerConfig { BootstrapServers = appSettings.KafkaBootstrapServers });
builder.Services.AddKafkaConsumersFactory();
builder.Services.AddTelegramTBot(botBuilder => botBuilder
    .AddSettings(new BotSettings(appSettings.TelegramBotToken))
    .AddRedisLimiter("localhost,password=password,defaultDatabase=0"));

var app = builder.Build();

IKafkaConsumerFactory kafkaConsumerFactory = app.Services.GetRequiredService<IKafkaConsumerFactory>();
ConsumerConfig consumerConfig = new ConsumerConfig{BootstrapServers = appSettings.KafkaBootstrapServers};
kafkaConsumerFactory.Subscribe<RequestEvent, RequestHandler>(consumerConfig);

app.MapGet("/", () => "Kyoto.Telegram.Sender 0.1");
await app.RunAsync();