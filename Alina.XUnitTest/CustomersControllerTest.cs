using Alinta.Api.Models;
using Alinta.Repository;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using Alinta.Api.Controllers;
using Customer = Alinta.Api.Models.Customer;
using CustomerEntity = Alinta.Data.Entities.Customer;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Linq.Expressions;

namespace Alinta.Test
{
    public class CustomersControllerTest
    {

        private Mock<IUnitOfWork> _mockUoW;
        private Mock<ICustomerRepository> _mockCustomerRepository;
        private Mock<IMapper> _mockMapper;
        private List<CustomerEntity> _entityCustomer;
        private Mock<ILogger<CustomersController>> _mockLogger;
        private List<Customer> _modelCustomer;

        public CustomersControllerTest()
        {

            _mockUoW = new Mock<IUnitOfWork>();
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _mockMapper = new Mock<IMapper>();
            _entityCustomer = new List<CustomerEntity>();
            _mockLogger = new Mock<ILogger<CustomersController>>();
            _modelCustomer = new List<Customer>();

            _entityCustomer.Add(new CustomerEntity(1, "First Name e1", "Last Name e1", new DateTime(2016, 7, 1)));
            _entityCustomer.Add(new CustomerEntity(2, "First Name e2", "Last Name e2", new DateTime(2016, 7, 2)));

            _modelCustomer.Add(new Customer(1, "First Name m1", "Last Name m1", new DateTime(2016, 7, 1)));
            _modelCustomer.Add(new Customer(2, "First Name m2", "Last Name m2", new DateTime(2016, 7, 2)));

        }

        [Fact]
        public void Get_WhenGettingAllCustomers_ReturnsAllCustomers()
        {
            //// Arrange
            _mockMapper.Setup(x => x.Map<IEnumerable<Customer>>(It.IsAny<IEnumerable<CustomerEntity>>()))
                        .Returns(_modelCustomer);
            _mockUoW.Setup(p => p.CustomerRepository).Returns(_mockCustomerRepository.Object);
            _mockCustomerRepository.Setup(p => p.GetAll()).Returns(_entityCustomer);

            var controller = new CustomersController(_mockUoW.Object, _mockLogger.Object, _mockMapper.Object);
            
            //// Act
            var result = controller.GetCustomers();

            //// Assert
            Assert.Equal(_modelCustomer, result);

        }

        [Fact]
        public void Get_WhenGetCustomerById_CustomerIsReturned()
        {
            //// Arrange
            _mockMapper.Setup(x => x.Map<Customer>(It.IsAny<CustomerEntity>()))
                   .Returns(_modelCustomer.First());

            _mockUoW.Setup(p => p.CustomerRepository).Returns(_mockCustomerRepository.Object);
            _mockCustomerRepository.Setup(p => p.Get(It.IsAny<long>())).Returns(_entityCustomer.First());

            var controller = new CustomersController(_mockUoW.Object, _mockLogger.Object, _mockMapper.Object);

            //// Act
            var result = controller.GetCustomer(1);

            _mockCustomerRepository.Verify(p => p.Get(It.IsAny<long>()), Times.Exactly(1));


            //// Assert
            Assert.Equal(_modelCustomer.First().ID, result.Result.Value.ID);


        }


    }
}
