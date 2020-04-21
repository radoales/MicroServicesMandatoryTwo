using CustomerManagement.Models;
using CustomerManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _customerService.GetCustomers();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            return await _customerService.GetCustomer(id);
        }

        // UPDATE: api/Customers/5
        [HttpPut("{id}")]
        public async Task<HttpResponseMessage> UpdateCustomer(int id, Customer customer)
        {
            return await _customerService.UpdateCustomer(id, customer);
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<HttpResponseMessage> PostCustomer(Customer customer)
        {
            return await _customerService.CreateCustomer(customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<HttpResponseMessage> DeleteCustomer(int id)
        {
            return await _customerService.DeleteCustomer(id);
        }


    }
}
