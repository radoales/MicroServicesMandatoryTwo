using CustomerManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerManagement.Test.SetUps
{
    public static class DataBaseSetUp
    {
        public static CustomerManagementContext GetDB()
        {
            var dboptions = new DbContextOptionsBuilder<CustomerManagementContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new CustomerManagementContext(dboptions);
        }
    }
}
