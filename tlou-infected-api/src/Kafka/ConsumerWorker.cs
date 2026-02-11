using Confluent.Kafka;

namespace tlou_infected_api.Kafka;

public class ConsumerWorker : BackgroundService
{
    private readonly ILogger<ConsumerWorker> _log;
    private readonly IConfiguration _config;
    private readonly string _host;
    private readonly string _topic;

    public ConsumerWorker(ILogger<ConsumerWorker> log, IConfiguration config)
    {
        _log = log;
        _config = config;
        _host = _config.GetSection("Kafka:Host").Value;
        _topic = _config.GetSection("Kafka:Topic").Value;
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

        using var consumer = new ConsumerBuilder<string, string>(config).Build();
        
        consumer.Subscribe(_topic);
        _log.LogInformation($"Kafka Consumer inscrito no tópico: {_topic}");

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
                    await Task.Delay(100, stoppingToken);
                }
                catch (ConsumeException ex)
                {
                    _log.LogError($"Erro ao consumir mensagem: {ex.Message}");
                }catch (OperationCanceledException)
                {
                    break;
                }
            }
        }
        finally
        {
            consumer.Close();
            _log.LogInformation("Kafka Consumer encerrado");
        }
    }
}