using Microsoft.EntityFrameworkCore;

namespace CustomerManagement.Models
{
    public class CustomerManagementContext : DbContext
    {
        public CustomerManagementContext(DbContextOptions<CustomerManagementContext> options) 
            : base(options)
        {
            
        }

        public CustomerManagementContext()
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
    }
}
