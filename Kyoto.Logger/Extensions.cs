using Confluent.Kafka;
using Kyoto.Domain.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Kafka;

namespace Kyoto.Logger;

public static class Extensions
{
    public static void AddLogger(this ILoggingBuilder loggingBuilder, IConfiguration configuration, KafkaSettings kafkaSettings, string topic = "kafka_bot_factory")
    {
        loggingBuilder.ClearProviders();
        loggingBuilder.AddConsole();
        loggingBuilder.AddSerilog(new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .WriteTo.Kafka(
                bootstrapServers: kafkaSettings.BootstrapServers,
                formatter: new GraylogFormatter(),
                securityProtocol: SecurityProtocol.Plaintext,
                saslMechanism: SaslMechanism.Plain,
                topic: topic
            )
            .Enrich.WithThreadId()
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithProcessId()
            .Enrich.WithAssemblyName()
            .Enrich.WithAssemblyVersion()
            .Enrich.WithClientIp()
            .Enrich.WithClientAgent()
            .CreateLogger());
    }
}