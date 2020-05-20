using System.Collections.Generic;

namespace CustomerManagement.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool HasInvoices { get; set; }
        public IEnumerable<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
