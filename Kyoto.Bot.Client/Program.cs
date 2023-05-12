using Confluent.Kafka;
using Kyoto.DI;
using Kyoto.Extensions;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Handlers;
using Kyoto.Kafka.Interfaces;
using Kyoto.Logger;
using Kyoto.Settings;
using InitTenantHandler = Kyoto.Bot.Client.KafkaHandlers.InitTenantHandler;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSettings<DatabaseSettings>(builder.Configuration, out var databaseSettings);
builder.Services.AddSettings<KafkaSettings>(builder.Configuration, out var kafkaSettings);

//Infrastructure
builder.Services
    .AddDeploy()
    .AddDatabaseBotClient(databaseSettings)
    .AddBotClientDeployStatus()
    .AddKafka(kafkaSettings);

//Functional
builder.Services
    .AddMenu()
    .AddProcessorServices()
    .AddCommandSystem()
    .AddClientCommands()
    .AddPostService();

//Logging
builder.Logging.AddLogger(builder.Configuration, kafkaSettings);

var app = builder.Build();
var kafkaConsumerFactory = app.Services.GetRequiredService<IKafkaConsumerFactory>();

await app.Services.InitBotTenantsAsync();

var consumerConfig = new ConsumerConfig { BootstrapServers = kafkaSettings.BootstrapServers };
await kafkaConsumerFactory.SubscribeAsync<InitTenantEvent, InitTenantHandler>(consumerConfig);
await kafkaConsumerFactory.SubscribeAsync<DeployStatusEvent, DeployStatusHandler>(consumerConfig, groupId: $"{nameof(DeployStatusHandler)}-Client");

await app.RunAsync();