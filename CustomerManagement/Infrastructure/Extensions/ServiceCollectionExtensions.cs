namespace CustomerManagement.Infrastructure.Extensions
{
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
            services.AddSingleton<IKafkaService, KafkaService>();

            return services;
        }

        public static IServiceCollection AddDatabaseConnection(this IServiceCollection services, IConfiguration configuration)
           => services.AddDbContext<CustomerManagementContext>(options =>
           options.UseSqlite(configuration.GetConnectionString("SqlConnection")));
    }
}
