using Confluent.Kafka;
using Kyoto.DI;
using Kyoto.Extensions;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Handlers;
using Kyoto.Kafka.Interfaces;
using Kyoto.Logger;
using Kyoto.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSettings<DatabaseSettings>(builder.Configuration, out var databaseSettings);
builder.Services.AddSettings<KafkaSettings>(builder.Configuration, out var kafkaSettings);

//Infrastructure
builder.Services
    .AddKafka(kafkaSettings);

//Functional
builder.Services
    .AddProcessorServices()
    .AddFactoryCommands()
    .AddExecutiveCommand()
    .AddPostService();

//Logging
builder.Logging.AddLogger(builder.Configuration, kafkaSettings);

var app = builder.Build();
var kafkaConsumerFactory = app.Services.GetRequiredService<IKafkaConsumerFactory>();

await app.Services.InitBotTenantsAsync();
await kafkaConsumerFactory.SubscribeAsync<InitTenantEvent, InitTenantHandler>(new ConsumerConfig { BootstrapServers = kafkaSettings.BootstrapServers });

await app.RunAsync();