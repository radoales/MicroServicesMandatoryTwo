using CustomerManagement.Models;
using System.Threading.Tasks;

namespace CustomerManagement.Services
{
    public interface IKafkaService
    {
        public Task Produce(string topic, object value);
        public void Consume(string topic);
    }
}
