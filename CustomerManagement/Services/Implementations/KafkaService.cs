namespace CustomerManagement.Services.Implementations
{
    using Confluent.Kafka;
    using Newtonsoft.Json;
    using System.Threading;
    using System.Threading.Tasks;

    public class KafkaService : IKafkaService
    {
        public async Task Produce(string topic, object value)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            string serializedObject = JsonConvert.SerializeObject(value);

            // Create a producer that can be used to send messages to kafka that have no key and a value of type string 
            using var producer = new ProducerBuilder<Null, string>(config).Build();

            // Construct the message to send (generic type must match what was used above when creating the producer)
            var message = new Message<Null, string>
            {
                Value = serializedObject
            };

            // Send the message to topic in Kafka                
            var dr = await producer.ProduceAsync(topic, message, CancellationToken.None);
        }
    }
}


