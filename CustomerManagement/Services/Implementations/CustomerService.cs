using CustomerManagement.Models;
using CustomerManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerManagement.Implementations.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerManagementContext _context;

        public CustomerService(CustomerManagementContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                throw new ArgumentException("Not Found"); //todo: Get some Exception Handling
            }

            return customer;
        }

        public async Task<HttpResponseMessage> UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<int> CreateCustomer(Customer customer)
        {

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer.Id;
        }

    public async Task<HttpResponseMessage> DeleteCustomer(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
        {
            return new HttpResponseMessage(System.Net.HttpStatusCode.NotFound);
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return new HttpResponseMessage(System.Net.HttpStatusCode.OK);
    }

    public bool CustomerExists(int id)
    {
        return _context.Customers.Any(e => e.Id == id);
    }
}
}
