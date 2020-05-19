namespace CustomerManagement.Test.ControllersTests
{
    using CustomerManagement.Controllers;
    using System.Threading.Tasks;
    using Xunit;
    using FluentAssertions;
    using System.Collections.Generic;
    using CustomerManagement.Models;
    using Moq;
    using CustomerManagement.Implementations.Services;
    using static CustomerManagement.Test.SetUps.DataBaseSetUp;
    using Microsoft.AspNetCore.Mvc;

    public class CustomersControllerTest
    {
        [Fact]
        public async Task GetCustomers_ShouldReturn_ListOfCustomers()
        {
            //Arrange
            var customerService = new Mock<CustomerService>(GetDB()).Object;

            var customersController = new CustomersController(customerService,null);
            //Act
            var result = await customersController.GetCustomers();
            //Assert
            result
                .Should()
                .BeOfType<ActionResult<IEnumerable<Customer>>>();
        }
    }
}
