using Kyoto.Bot.StartUp;

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

var app = builder.Build();

app.SubscribeToEvents(appSettings);
await app.Services.PrepareDatabaseAsync(appSettings.DatabaseSettings);
app.MapGet("/", () => "Kyoto bot! 0.3");

await app.RunAsync();