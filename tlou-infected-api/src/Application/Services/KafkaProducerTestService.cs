using Confluent.Kafka;

namespace tlou_infected_api.Application.Services;

public interface IKafkaProducerService
{
    Task SendMessageAsync(string topic, string message);
}

public class KafkaProducerTestService : IKafkaProducerService
{
        private readonly IProducer<string, string> _producer;

        public KafkaProducerTestService()
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task SendMessageAsync(string topic, string message)
        {
            var dr = await _producer.ProduceAsync(topic, new Message<string, string> { Value = message });
            Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
        }
}