using Confluent.Kafka;
using Kyoto.Bot.Client;
using Kyoto.Bot.Client.Middlewares;
using Kyoto.DI;
using Kyoto.Extensions;
using Kyoto.Kafka.Event;
using Kyoto.Kafka.Handlers;
using Kyoto.Kafka.Interfaces;
using Kyoto.Logger;
using Kyoto.Services.Tenant;
using Kyoto.Settings;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSettings<DatabaseSettings>(builder.Configuration, out var databaseSettings);
builder.Services.AddSettings<KafkaSettings>(builder.Configuration, out var kafkaSettings);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Kyoto Bot Client API"
    });
    
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            Array.Empty<string>()
        }
    });
});

//Infrastructure
builder.Services
    .AddControllers().Services
    .AddAuthorizationServices()
    .AddDatabaseBotClient(databaseSettings)
    .AddBotClientDeploy()
    .AddKafka(kafkaSettings)
    .AddTransient<IKafkaEventSubscriber, KafkaEventSubscriber>();

//Functional
builder.Services
    .AddMenu()
    .AddProcessorServices()
    .AddCommandSystem()
    .AddClientCommands()
    .AddPostService()
    .AddPreparedMessages()
    .AddTemplateMessage()
    .AddFeedback();

//Logging
builder.Logging.AddLogger(builder.Configuration, kafkaSettings);

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
app.UseStaticFiles();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseMiddleware<TenantIdentifierMiddleware>();


var kafkaConsumerFactory = app.Services.GetRequiredService<IKafkaConsumerFactory>();
var consumerConfig = new ConsumerConfig{BootstrapServers = kafkaSettings.BootstrapServers};
await kafkaConsumerFactory.SubscribeAsync<DeployStatusEvent, DeployStatusHandler>(consumerConfig, groupId: $"{nameof(DeployStatusHandler)}-Client");
await kafkaConsumerFactory.SubscribeAsync<InitTenantEvent, InitClientTenantHandler>(consumerConfig);
await kafkaConsumerFactory.SubscribeAsync<RemoveTenantEvent, RemoveTenantHandler>(consumerConfig, groupId: $"{nameof(RemoveTenantHandler)}-Client");

await app.Services.SendRequestBotTenantsAsync();

await app.RunAsync();