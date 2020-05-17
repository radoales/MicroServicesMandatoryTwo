namespace CustomerManagement.Services.Implementations
{
    using Confluent.Kafka;
    using CustomerManagement.Models;
    using Newtonsoft.Json;
    using System;
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
           // Console.WriteLine($"Produced message '{dr.Value}' to topic {dr.Topic}, partition {dr.Partition}, offset {dr.Offset}");
        }

        //public void Consume(string topic)
        //{
        //    var conf = new ConsumerConfig
        //    {
        //        GroupId = "test-consumer-group",
        //        BootstrapServers = "localhost:9092",
        //        AutoOffsetReset = AutoOffsetReset.Earliest
        //    };

        //    using var consummer = new ConsumerBuilder<Ignore, string>(conf).Build();
        //    consummer.Subscribe(topic);

        //    // Because Consume is a blocking call, we want to capture Ctrl+C and use a cancellation token to get out of our while loop and close the consumer gracefully.
        //    var cts = new CancellationTokenSource();
        //    Console.CancelKeyPress += (_, e) =>
        //    {
        //        e.Cancel = true;
        //        cts.Cancel();
        //    };

        //    try
        //    {
        //        while (true)
        //        {
        //            try
        //            {
        //                var cr = consummer.Consume(cts.Token);
        //                Console.WriteLine($"Consumed message '{cr.Message.Value}' from topic {cr.Topic}, partition {cr.Partition}, offset {cr.Offset}");
        //                Customer customer = JsonConvert.DeserializeObject<Customer>(cr.Message.Value);
        //                this.customerService.CreateCustomer(customer);
        //            }
        //            catch (ConsumeException e)
        //            {

        //                Console.WriteLine($"Error occured: {e.Error.Reason}");
        //            }

        //        }
        //    }
        //    catch (OperationCanceledException)
        //    {
        //        consummer.Close();
        //    }


        //    //finally
        //    //{
                
        //    //}
        //}
    }
}


