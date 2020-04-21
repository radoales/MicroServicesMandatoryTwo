﻿using CustomerManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CustomerManagement.Services.Interfaces
{
    public interface ICustomerService
    {
         Task<ActionResult<IEnumerable<Customer>>> GetCustomers();
         Task<ActionResult<Customer>> GetCustomer(int id);
         Task<HttpResponseMessage> UpdateCustomer(int id, Customer customer);
         Task<HttpResponseMessage> CreateCustomer(Customer customer);
         Task<HttpResponseMessage> DeleteCustomer(int id);
         bool CustomerExists(int id);
    }
}
