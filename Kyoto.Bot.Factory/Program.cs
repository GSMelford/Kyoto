using Kyoto.Bot.StartUp;
using Kyoto.Logger;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSettings(builder.Configuration, out var appSettings)
    .AddKafka(appSettings)
    .AddDatabase(appSettings.DatabaseSettings)
    .AddAuthorizationServices()
    .AddMenu()
    .AddCommandServices()
    .AddPostServices()
    .AddOtherServices();

builder.Logging.AddLogger(builder.Configuration, appSettings.KafkaBootstrapServers);
var app = builder.Build();

await app.SubscribeToEventsAsync(appSettings);
await app.Services.InitBotTenantsAsync();
await app.Services.PrepareDatabaseAsync(appSettings.DatabaseSettings);
app.MapGet("/", () => "Kyoto bot! 0.3");

await app.RunAsync();