using Confluent.Kafka;
using Kyoto.Kafka;
using Kyoto.Logger;
using Kyoto.Telegram.Receiver;
using Kyoto.Telegram.Receiver.Interfaces;
using Kyoto.Telegram.Receiver.Services;

var builder = WebApplication.CreateBuilder(args);

AppSettings appSettings = new AppSettings();
builder.Configuration.Bind(nameof(AppSettings), appSettings);
builder.Services.AddSingleton(appSettings);
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddTransient<IUpdateService, UpdateService>();
builder.Services.AddTransient<IMessageDistributorService, MessageDistributorService>();
builder.Services.AddKafkaProducer<string>(new ProducerConfig { BootstrapServers = appSettings.KafkaBootstrapServers });
builder.Services.AddKafkaConsumersFactory();
builder.Logging.AddLogger(builder.Configuration, appSettings.KafkaBootstrapServers);

var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.MapHealthChecks("/health");
app.MapGet("/", () => "Kyoto.Telegram.Receiver 0.1");

await app.RunAsync();