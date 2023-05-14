using Confluent.Kafka;
using Kyoto.Bot.Client;
using Kyoto.DI;
using Kyoto.Extensions;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Handlers;
using Kyoto.Kafka.Interfaces;
using Kyoto.Logger;
using Kyoto.Services.Tenant;
using Kyoto.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSettings<DatabaseSettings>(builder.Configuration, out var databaseSettings);
builder.Services.AddSettings<KafkaSettings>(builder.Configuration, out var kafkaSettings);

//Infrastructure
builder.Services
    .AddDatabaseBotClient(databaseSettings)
    .AddBotClientDeploy()
    .AddKafka(kafkaSettings)
    .AddTransient<IKafkaEventSubscriber, KafkaEventSubscriber>();

//Functional
builder.Services
    .AddMenu()
    .AddProcessorServices()
    .AddCommandSystem()
    .AddClientCommands()
    .AddPostService()
    .AddTemplateMessage();

//Logging
builder.Logging.AddLogger(builder.Configuration, kafkaSettings);

var app = builder.Build();

var kafkaConsumerFactory = app.Services.GetRequiredService<IKafkaConsumerFactory>();
var consumerConfig = new ConsumerConfig{BootstrapServers = kafkaSettings.BootstrapServers};
await kafkaConsumerFactory.SubscribeAsync<DeployStatusEvent, DeployStatusHandler>(consumerConfig, groupId: $"{nameof(DeployStatusHandler)}-Client");
await kafkaConsumerFactory.SubscribeAsync<InitTenantEvent, InitClientTenantHandler>(consumerConfig);

await app.Services.SendRequestBotTenantsAsync();

await app.RunAsync();