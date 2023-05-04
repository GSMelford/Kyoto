using Confluent.Kafka;
using Kyoto.Kafka;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;
using Kyoto.Logger;
using Kyoto.Telegram.Sender;
using Kyoto.Telegram.Sender.Interfaces;
using Kyoto.Telegram.Sender.KafkaHandlers;
using Kyoto.Telegram.Sender.Services;
using TBot.Client.AspNet;

var builder = WebApplication.CreateBuilder(args);

AppSettings appSettings = new AppSettings();
builder.Configuration.Bind(nameof(AppSettings), appSettings);
builder.Services.AddSingleton(appSettings);
builder.Services.AddTransient<IRequestService, RequestService>();
builder.Services.AddKafkaProducer<string>(new ProducerConfig { BootstrapServers = appSettings.KafkaBootstrapServers });
builder.Services.AddKafkaConsumersFactory();
builder.Logging.AddLogger(builder.Configuration, appSettings.KafkaBootstrapServers);
builder.Services.AddTelegramTBot();
builder.Services.AddTBotRedisLimiter("localhost,password=password,defaultDatabase=0");

var app = builder.Build();

var kafkaConsumerFactory = app.Services.GetRequiredService<IKafkaConsumerFactory>();
var consumerConfig = new ConsumerConfig{BootstrapServers = appSettings.KafkaBootstrapServers};
await kafkaConsumerFactory.SubscribeAsync<InitTenantEvent, InitTenantHandler>(consumerConfig, groupId: $"{nameof(InitTenantHandler)}-Sender");

var kafkaProducer = app.Services.GetRequiredService<IKafkaProducer<string>>();
await kafkaProducer.ProduceAsync(new RequestTenantEvent { SessionId = Guid.NewGuid() }, string.Empty);

app.MapGet("/", () => "Kyoto.Telegram.Sender 0.1");
await app.RunAsync();