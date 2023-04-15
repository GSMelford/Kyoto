using Kyoto.Bot.CommandServices;
using Kyoto.Bot.CommandServices.ExecutiveCommand;
using Kyoto.Bot.Services.Authorization;
using Kyoto.Bot.Services.Processors;
using Kyoto.Bot.Services.RequestSender;
using Kyoto.Bot.StartUp;
using Kyoto.Domain.Authorization;
using Kyoto.Domain.BotUser;
using Kyoto.Domain.CommandServices;
using Kyoto.Domain.ExecutiveCommand;
using Kyoto.Domain.Processors;
using Kyoto.Domain.RequestSender;
using Kyoto.Infrastructure.Repositories.Authorization;
using Kyoto.Infrastructure.Repositories.BotUser;
using Kyoto.Infrastructure.Repositories.ExecutiveCommand;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSettings(builder.Configuration, out var appSettings)
    .AddKafka(appSettings)
    .AddDatabase(appSettings.DatabaseSettings)
    .AddTransient<IRequestService, RequestService>()
    .AddTransient<IUserRepository, UserRepository>()
    .AddTransient<IAuthorizationRepository, AuthorizationRepository>()
    .AddTransient<IAuthorizationService, AuthorizationService>()
    .AddTransient<IStartCommandService, StartCommandService>()
    .AddTransient<IMessageService, MessageService>()
    .AddTransient<IExecutiveTelegramCommandService, ExecutiveTelegramCommandService>()
    .AddTransient<IExecutiveTelegramCommandRepository, ExecutiveTelegramCommandRepository>();

var app = builder.Build();

app.SubscribeToEvents(appSettings);
await app.Services.PrepareDatabaseAsync(appSettings.DatabaseSettings);
app.MapGet("/", () => "Kyoto bot! 0.1");

await app.RunAsync();