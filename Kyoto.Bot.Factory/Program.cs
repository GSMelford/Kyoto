using Kyoto.Bot;
using Kyoto.DI;
using Kyoto.Extensions;
using Kyoto.Logger;
using Kyoto.Services.Tenant;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSettings(builder.Configuration, out var appSettings);

//Infrastructure
builder.Services
    .AddKafka(appSettings.KafkaSettings)
    .AddDatabase(appSettings.DatabaseSettings)
    .AddTenant(appSettings.BotTenantSettings);

//Functional
builder.Services
    .AddAuthorizationServices()
    .AddMenu()
    .AddBot()
    .AddProcessorServices()
    .AddPostService()
    .AddUser()
    .AddFactoryCommands()
    .AddGlobalCommands()
    .AddExecutiveCommand()
    .AddPostServices()
    .AddHttpServices();

//KafkaEventSubscriber
builder.Services
    .AddTransient<IKafkaEventSubscriber, KafkaEventSubscriber>();

//HttpServices
builder.Services
    .AddHttpServices();

//Logging
builder.Logging.AddLogger(builder.Configuration, appSettings.KafkaSettings);

var app = builder.Build();

await app.Services.PrepareDatabaseAsync(appSettings.DatabaseSettings);
await app.Services.SubscribeToEventsAsync(appSettings.KafkaSettings);
await app.Services.InitBotTenantsAsync();

await app.RunAsync();