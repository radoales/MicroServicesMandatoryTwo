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
         Task<bool> UpdateCustomer(int id, string email, string firstName, string lastName);
         Task<int> CreateCustomer(Customer customer);
         Task<HttpResponseMessage> DeleteCustomer(int id);
         bool CustomerExists(int id);
    }
}
