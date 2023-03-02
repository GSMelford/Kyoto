using Kyoto.Telegram.Receiver.Interfaces;
using Kyoto.Telegram.Receiver.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<IUpdateService, UpdateService>();

var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.MapHealthChecks("/health");

await app.RunAsync();