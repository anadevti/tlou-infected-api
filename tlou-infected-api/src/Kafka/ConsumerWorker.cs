using System.Text.Json;
using Confluent.Kafka;
using tlou_infected_api.Application.Services;
using tlou_infected_api.Domain.DTO.Survivor;

namespace tlou_infected_api.Kafka;

public class ConsumerWorker : BackgroundService
{
    private readonly ILogger<ConsumerWorker> _log;
    private readonly IConfiguration _config;
    private readonly string _host;
    private readonly string _topic;
    private readonly string _topicSurvivor;
    private readonly IServiceScopeFactory _scopeFactory;

    public ConsumerWorker(ILogger<ConsumerWorker> log, IConfiguration config, IServiceScopeFactory scopeFactory)
    {
        _log = log;
        _config = config;
        _scopeFactory = scopeFactory;
        _host = _config.GetSection("Kafka:Host").Value;
        _topic = _config.GetSection("Kafka:Topic").Value;
        _topicSurvivor = _config.GetSection("Kafka:TopicSurvivor").Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = _host,
            GroupId = $"{_topic}-group-0",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };
        var configSurvivor = new ConsumerConfig
        {
            BootstrapServers = _host,
            GroupId = $"{_topicSurvivor}-group-1",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };

        using var consumer = new ConsumerBuilder<string, string>(config).Build();
        using var consumerSurvivor = new ConsumerBuilder<string, string>(configSurvivor).Build();
        consumer.Subscribe(_topic);
        _log.LogInformation($"Kafka Consumer inscrito no tópico: {_topic}");
        consumerSurvivor.Subscribe(_topicSurvivor);
        _log.LogInformation($"Kafka Consumer Survivor inscrito no tópico: {_topicSurvivor}");

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = consumer.Consume(TimeSpan.FromSeconds(1));
                    if (consumeResult != null)
                    {
                        _log.LogInformation($"Mensagem recebida: {consumeResult.Message.Value}");
                        consumer.Commit(consumeResult);
                    }

                    var consumeResultSurvivor = consumerSurvivor.Consume(TimeSpan.FromSeconds(1));
                    if (consumeResultSurvivor != null)
                    {
                        _log.LogInformation($"Mensagem Survivor recebida: {consumeResultSurvivor.Message.Value}");
                        var dto = JsonSerializer.Deserialize<SurvivorDto>(consumeResultSurvivor.Message.Value);
                        if (dto != null)
                        {
                            // Cria um escopo para resolver serviços scoped (SurvivorService, repositórios, etc.)
                            using var scope = _scopeFactory.CreateScope();
                            var service = scope.ServiceProvider.GetRequiredService<SurvivorService>();
                            await service.Create(dto);
                        }
                        consumerSurvivor.Commit(consumeResultSurvivor);
                    }

                    await Task.Delay(100, stoppingToken);
                }
                catch (ConsumeException ex)
                {
                    _log.LogError($"Erro ao consumir mensagem: {ex.Message}");
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
        finally
        {
            consumer.Close();
            consumerSurvivor.Close();
            _log.LogInformation("Kafka Consumers encerrados");
        }
    }
}