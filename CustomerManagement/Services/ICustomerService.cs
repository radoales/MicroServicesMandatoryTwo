using CustomerManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerManagement.Services
{
    public interface ICustomerService
    {
         Task<ActionResult<IEnumerable<Customer>>> GetCustomers();
         Task<ActionResult<Customer>> GetCustomer(int id);
         Task<HttpResponseMessage> UpdateCustomer(int id, Customer customer);
         Task<int> CreateCustomer(Customer customer);
         Task<HttpResponseMessage> DeleteCustomer(int id);
         bool CustomerExists(int id);
    }
}
