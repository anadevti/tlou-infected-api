// using Confluent.Kafka;
//
// namespace tlou_infected_api.Kafka;
//
// public class ProducerWorker : BackgroundService
// {
//     private readonly ILogger<ProducerWorker> _logger;
//     private readonly IConfiguration _config;
//     private readonly string _host;
//     private readonly string _topic;
//
//     public ProducerWorker(ILogger<ProducerWorker> logger, IConfiguration config)
//     {
//         _logger = logger;
//         _config = config;
//         _host = _config.GetSection("Kafka:Host").Value;
//         _topic = _config.GetSection("Kafka:Topic").Value;
//     }
//     protected override Task ExecuteAsync(CancellationToken stoppingToken)
//     {
//         try
//         {
//             var config = new ProducerConfig { BootstrapServers = _host };
//             using (var producer = new ProducerBuilder<string, string>(config).Build())
//             {
//                 var result = producer.ProduceAsync(_topic,
//                     new Message<string, string>
//                         { Key = $"KEY_{1}",
//                             Value = Guid.NewGuid().ToString()
//                         });
//             }
//         }
//         catch (Exception e)
//         {
//             Console.WriteLine(e);
//             throw;
//         }
//         return Task.CompletedTask;
//     }
// }