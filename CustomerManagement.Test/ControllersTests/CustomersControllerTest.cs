namespace CustomerManagement.Test.ControllersTests
{
    using CustomerManagement.Controllers;
    using CustomerManagement.Controllers.RequestModels;
    using CustomerManagement.Models;
    using CustomerManagement.Services;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class CustomersControllerTest
    {
        [Fact]
        public async Task GetCustomers_ShouldReturn_ActionResultWithListOfCustomers()
        {
            //Arrange
            var customerService = new Mock<ICustomerService>();
            customerService
                .Setup(x => x.GetCustomers())
                .ReturnsAsync(new List<Customer>());

            var customersController = new CustomersController(customerService.Object, null);

            //Act
            var result = await customersController.GetCustomers();

            //Assert
            result
                .Should()
                .BeOfType<ActionResult<IEnumerable<Customer>>>();
        }

        [Fact]
        public async Task GetCustomer_ShouldReturn_ActionResultWithCustomer()
        {
            //Arrange
            var customerService = new Mock<ICustomerService>();

            customerService
            .Setup(x => x.GetCustomer(It.IsAny<int>()))
            .ReturnsAsync(new Customer());

            var customersController = new CustomersController(customerService.Object, null);

            //Act
            var result = await customersController.GetCustomer(1);

            //Assert
            result
                .Should()
                .BeOfType<ActionResult<Customer>>();
        }

        [Fact]
        public async Task UpdateCustomer_ShouldReturn_OkResult()
        {
            //Arrange
            var customerServiceMock = new Mock<ICustomerService>();
            var kafkaServiceMock = new Mock<IKafkaService>();

            customerServiceMock
            .Setup(x => x.UpdateCustomer(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(true);

            var customersController = new CustomersController(customerServiceMock.Object, kafkaServiceMock.Object);

            var model = new UpdateCustomerRequestModel();

            //Act
            var result = await customersController.UpdateCustomer(model);

            //Assert
            result
                .Should()
                .BeOfType<OkResult>();
        }

        [Fact]
        public async Task UpdateCustomer_ShouldReturn_BadrequestResult()
        {
            //Arrange
            var customerService = new Mock<ICustomerService>();

            customerService
            .Setup(x => x.UpdateCustomer(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(false);

            var customersController = new CustomersController(customerService.Object, null);

            var model = new UpdateCustomerRequestModel();

            //Act
            var result = await customersController.UpdateCustomer(model);

            //Assert
            result
                .Should()
                .BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task DeleteCustomer_ShouldReturn_OkResult()
        {
            //Arrange
            var customerServiceMock = new Mock<ICustomerService>();
            var kafkaServiceMock = new Mock<IKafkaService>();

            customerServiceMock
            .Setup(x => x.DeleteCustomer(It.IsAny<int>()))
            .ReturnsAsync(true);

            var customersController = new CustomersController(customerServiceMock.Object, kafkaServiceMock.Object);

            //Act
            var result = await customersController.DeleteCustomer(1);

            //Assert
            result
                .Should()
                .BeOfType<OkResult>();
        }

        [Fact]
        public async Task DeleteCustomer_ShouldReturn_BadrequestResult()
        {
            //Arrange
            var customerService = new Mock<ICustomerService>();

            customerService
            .Setup(x => x.DeleteCustomer(It.IsAny<int>()))
            .ReturnsAsync(false);

            var customersController = new CustomersController(customerService.Object, null);

            //Act
            var result = await customersController.DeleteCustomer(1);

            //Assert
            result
                .Should()
                .BeOfType<BadRequestResult>();
        }


    }
}
