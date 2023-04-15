using Kyoto.Bot.Services.RequestSender;
using Kyoto.Bot.StartUp;
using Kyoto.Domain.RequestSender;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSettings(builder.Configuration, out var appSettings);
builder.Services.AddKafka(appSettings);
builder.Services.AddTransient<IRequestService, RequestService>();

var app = builder.Build();

app.SubscribeToEvents(appSettings);
app.MapGet("/", () => "Kyoto bot! 0.1");

await app.RunAsync();