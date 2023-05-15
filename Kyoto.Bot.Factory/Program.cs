using Confluent.Kafka;
using Kyoto.Bot.Factory;
using Kyoto.DI;
using Kyoto.Extensions;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Handlers;
using Kyoto.Kafka.Interfaces;
using Kyoto.Logger;
using Kyoto.Services.Tenant;
using Kyoto.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSettings<KyotoBotFactorySettings>(builder.Configuration, out _);
builder.Services.AddSettings<BotTenantSettings>(builder.Configuration, out var botTenantSettings);
builder.Services.AddSettings<DatabaseSettings>(builder.Configuration, out var databaseSettings);
builder.Services.AddSettings<KafkaSettings>(builder.Configuration, out var kafkaSettings);

//Infrastructure
builder.Services
    .AddKafka(kafkaSettings)
    .AddDatabaseBotFactory(databaseSettings, botTenantSettings.Key)
    .AddBotFactoryDeploy()
    .AddTenant(botTenantSettings)
    .AddRequestService()
    .AddTransient<IKafkaEventSubscriber, KafkaEventSubscriber>();

//Functional
builder.Services
    .AddAuthorizationServices()
    .AddMenu()
    .AddBot()
    .AddProcessorServices()
    .AddPostService()
    .AddUser()
    .AddFactoryCommands()
    .AddCommandSystem()
    .AddTemplateMessage();

//HttpServices
builder.Services
    .AddHttpServices();

//Logging
builder.Logging.AddLogger(builder.Configuration, kafkaSettings);

var app = builder.Build();

var kafkaConsumerFactory = app.Services.GetRequiredService<IKafkaConsumerFactory>();
var consumerConfig = new ConsumerConfig{BootstrapServers = kafkaSettings.BootstrapServers};
await kafkaConsumerFactory.SubscribeAsync<InitTenantEvent, InitFactoryTenantHandler>(consumerConfig);
await kafkaConsumerFactory.SubscribeAsync<RemoveTenantEvent, RemoveTenantHandler>(consumerConfig, groupId: $"{nameof(RemoveTenantHandler)}-Factory");

await app.Services.SubscribeToRequestTenantEventAsync(kafkaSettings);
await app.Services.SendRequestBotTenantsAsync();

await app.RunAsync();