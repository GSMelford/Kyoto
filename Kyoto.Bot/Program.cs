using Kyoto.Bot.Services.RequestSender;
using Kyoto.Bot.StartUp;
using Kyoto.Domain.BotUser;
using Kyoto.Domain.RequestSender;
using Kyoto.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSettings(builder.Configuration, out var appSettings)
    .AddKafka(appSettings)
    .AddDatabase(appSettings.DatabaseSettings)
    .AddTransient<IRequestService, RequestService>()
    .AddTransient<IUserRepository, UserRepository>();

var app = builder.Build();

app.SubscribeToEvents(appSettings);
await app.Services.PrepareDatabaseAsync(appSettings.DatabaseSettings);
app.MapGet("/", () => "Kyoto bot! 0.1");

await app.RunAsync();