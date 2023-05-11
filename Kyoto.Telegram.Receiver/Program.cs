using Confluent.Kafka;
using Kyoto.Kafka;
using Kyoto.Logger;
using Kyoto.Settings;
using Kyoto.Telegram.Receiver.Interfaces;
using Kyoto.Telegram.Receiver.Services;

var builder = WebApplication.CreateBuilder(args);

var kafkaSettings = new KafkaSettings();
builder.Configuration.Bind(nameof(KafkaSettings), kafkaSettings);
builder.Services.AddSingleton(kafkaSettings);
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddTransient<IUpdateService, UpdateService>();
builder.Services.AddTransient<IMessageDistributorService, MessageDistributorService>();
builder.Services.AddKafkaProducer<string>(new ProducerConfig { BootstrapServers = kafkaSettings.BootstrapServers });
builder.Services.AddKafkaConsumersFactory();
builder.Logging.AddLogger(builder.Configuration, kafkaSettings);

var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.MapHealthChecks("/health");
app.MapGet("/", () => "Kyoto.Telegram.Receiver 0.1");

await app.RunAsync();