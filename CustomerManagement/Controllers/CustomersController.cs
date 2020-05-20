namespace CustomerManagement.Controllers
{
    using CustomerManagement.Controllers.RequestModels;
    using CustomerManagement.Models;
    using CustomerManagement.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IKafkaService _kafkaService;

        public CustomersController(ICustomerService customerService, IKafkaService kafkaService)
        {
            _customerService = customerService;
            _kafkaService = kafkaService;
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
        public async Task<ActionResult> UpdateCustomer(UpdateCustomerRequestModel model)
        {
            var isUpdated = await this._customerService.UpdateCustomer(
               model.Id,
               model.Email,
               model.FirstName,
               model.LastName);

            if (!isUpdated)
            {
                return BadRequest();
            }

            await _kafkaService.Produce("EditCustomer", model);

            return Ok();
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult> PostCustomer(Customer customer)
        {
            var result = await _customerService.CreateCustomer(customer);

            await _kafkaService.Produce("AddCustomer", customer);

            return Created(nameof(PostCustomer), customer.Id);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<HttpResponseMessage> DeleteCustomer(int id)
        {
            await _kafkaService.Produce("DeleteCustomer", id);

            return await _customerService.DeleteCustomer(id);
        }


    }
}
