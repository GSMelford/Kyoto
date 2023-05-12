using Kyoto.DI;
using Kyoto.Extensions;
using Kyoto.Logger;
using Kyoto.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSettings<KyotoBotFactorySettings>(builder.Configuration, out _);
builder.Services.AddSettings<BotTenantSettings>(builder.Configuration, out var botTenantSettings);
builder.Services.AddSettings<DatabaseSettings>(builder.Configuration, out var databaseSettings);
builder.Services.AddSettings<KafkaSettings>(builder.Configuration, out var kafkaSettings);

//Infrastructure
builder.Services
    .AddKafka(kafkaSettings)
    .AddDatabaseBotFactory(databaseSettings)
    .AddBotFactoryDeployStatus()
    .AddTenant(botTenantSettings);

//Functional
builder.Services
    .AddAuthorizationServices()
    .AddMenu()
    .AddBot()
    .AddProcessorServices()
    .AddPostService()
    .AddUser()
    .AddFactoryCommands()
    .AddCommandSystem();

//HttpServices
builder.Services
    .AddHttpServices();

//Logging
builder.Logging.AddLogger(builder.Configuration, kafkaSettings);

var app = builder.Build();

await app.Services.PrepareDatabaseAsync(databaseSettings);
await app.Services.SubscribeToEventsAsync(kafkaSettings, botTenantSettings.Key);
await app.Services.InitBotTenantsAsync();

await app.RunAsync();