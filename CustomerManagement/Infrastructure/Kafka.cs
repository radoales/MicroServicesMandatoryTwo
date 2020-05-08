namespace CustomerManagement.Infrastructure
{
    using Confluent.Kafka;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public class Kafka
    {
        public async Task Produce(string topic)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "localhost:9092"
            };

            // Create a producer that can be used to send messages to kafka that have no key and a value of type string 
            using var p = new ProducerBuilder<Null, string>(config).Build();

            var i = 0;

            while (true)
            {
                // Construct the message to send (generic type must match what was used above when creating the producer)
                var message = new Message<Null, string>
                {
                    Value = $"Message #{++i}"
                };

                // Send the message to our test topic in Kafka                
                var dr = await p.ProduceAsync(topic, message, CancellationToken.None);
                Console.WriteLine($"Produced message '{dr.Value}' to topic {dr.Topic}, partition {dr.Partition}, offset {dr.Offset}");

                Thread.Sleep(5000);
            }
        }

        public void Consume(string topic)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var c = new ConsumerBuilder<Ignore, string>(conf).Build();
            c.Subscribe(topic);

            // Because Consume is a blocking call, we want to capture Ctrl+C and use a cancellation token to get out of our while loop and close the consumer gracefully.
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) =>
            {
                e.Cancel = true;
                cts.Cancel();
            };

            try
            {
                while (true)
                {
                    // Consume a message from the test topic. Pass in a cancellation token so we can break out of our loop when Ctrl+C is pressed
                    var cr = c.Consume(cts.Token);
                    Console.WriteLine($"Consumed message '{cr.Value}' from topic {cr.Topic}, partition {cr.Partition}, offset {cr.Offset}");
                }
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                c.Close();
            }
        }
    }
}
