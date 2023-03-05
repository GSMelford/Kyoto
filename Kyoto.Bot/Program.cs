using Confluent.Kafka;
using Kyoto.Bot;
using Kyoto.Bot.KafkaHandlers;
using Kyoto.Bot.KafkaHandlers.CommandHandlers;
using Kyoto.Bot.Services.RequestSender;
using Kyoto.Domain.RequestSender;
using Kyoto.Kafka;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Interfaces;

var builder = WebApplication.CreateBuilder(args);

AppSettings appSettings = new AppSettings();
builder.Configuration.Bind(nameof(AppSettings), appSettings);
builder.Services.AddSingleton(appSettings);
builder.Services.AddKafkaProducer<string>(new ProducerConfig { BootstrapServers = appSettings.KafkaBootstrapServers });
builder.Services.AddKafkaConsumersFactory();
builder.Services.AddTransient<IRequestService, RequestService>();

var app = builder.Build();

IKafkaConsumerFactory kafkaConsumerFactory = app.Services.GetRequiredService<IKafkaConsumerFactory>();
ConsumerConfig consumerConfig = new ConsumerConfig{BootstrapServers = appSettings.KafkaBootstrapServers};
kafkaConsumerFactory.Subscribe<CommandEvent, CommandHandler>(consumerConfig);
kafkaConsumerFactory.Subscribe<StartCommandEvent, StartCommandMessageHandler>(consumerConfig);

app.MapGet("/", () => "Kyoto bot! 0.1");
await app.RunAsync();