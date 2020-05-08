namespace CustomerManagement.Infrastructure.Extensions
{
    using Confluent.Kafka;
    using CustomerManagement.Implementations.Services;
    using CustomerManagement.Models;
    using CustomerManagement.Services;
    using CustomerManagement.Services.Implementations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICustomerService, CustomerService>();

            var producerConfig = new ProducerConfig();
            var consumerConfig = new ConsumerConfig();

            configuration.Bind("producer", producerConfig);
            configuration.Bind("consumer", consumerConfig);

            services.AddSingleton<ProducerConfig>(producerConfig);
            services.AddSingleton<ConsumerConfig>(consumerConfig);

            services.AddScoped<IKafkaService, KafkaService>();

            return services;
        }

        public static IServiceCollection AddDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
           => services.AddDbContext<CustomerManagementContext>(options =>
           options.UseSqlite(configuration.GetConnectionString("SqlConnection")));
    }
}
