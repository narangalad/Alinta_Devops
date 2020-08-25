using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Alinta.Api.Controllers;
using Alinta.Repository;
using Alinta.Repository.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Customer = Alinta.Api.Models.Customer;
using CustomerEntity = Alinta.Data.Entities.Customer;


namespace Store.Tests
{
    public class CustomersControllerTests
    {
        private Mock<IUnitOfWork> _mockUoW;
        private Mock<ICustomerRepository> _mockCustomerRepository;
        private Mock<IMapper> _mockMapper;
        private List<CustomerEntity> _entityCustomers;
        private List<Customer> _modelCustomer;
        private Mock<ILogger<CustomersController>> _mockLogger;

        [SetUp]
        public void Setup()
        {
            _mockUoW = new Mock<IUnitOfWork>();
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _mockMapper = new Mock<IMapper>();
            _entityCustomers = new List<CustomerEntity>();
            _mockLogger = new Mock<ILogger<CustomersController>>();

            _entityCustomers.Add(new CustomerEntity(1, "Name 1", "Name 2",new DateTime(2016, 7, 2)));
            _entityCustomers.Add(new CustomerEntity(2, "Name 1", "Name 2", new DateTime(2016, 7, 2)));


            _modelCustomer.Add(new Customer(1, "Name 1", "Name 2", new DateTime(2016, 7, 2)));
            _modelCustomer.Add(new Customer(2, "Name 1", "Name 2", new DateTime(2016, 7, 2)));

        }

        [Test]
        public void Get_ReturnsAllCoustomoers()
        {
            _mockMapper.Setup(x => x.Map<IEnumerable<Customer>>(It.IsAny<IEnumerable<CustomerEntity>>()))
                .Returns(_modelCustomer);

            _mockUoW.Setup(p => p.CustomerRepository).Returns(_mockCustomerRepository.Object);
            _mockCustomerRepository.Setup(p => p.GetAll()).Returns(_entityCustomers);

            CustomersController controller = new CustomersController(_mockUoW.Object,  _mockLogger.Object, _mockMapper.Object);

            var result = controller.GetCustomers();

            Assert.AreEqual(_modelCustomer.Count, result.ToList().Count());
        }

        //[Test]
        //public void Post_WhenSavingValidProduct_ProductIsSaved()
        //{
        //    _mockMapper.Setup(x => x.Map<ProductEntity>(It.IsAny<Product>()))
        //        .Returns(_entityProducts.First());

        //    _mockUoW.Setup(p => p.ProductRepository).Returns(_mockProductRepository.Object);
        //    _mockProductRepository.Setup(p => p.Add(_entityProducts.First())).Verifiable();

        //    ProductsController controller = new ProductsController(_mockUoW.Object, _mockMapper.Object, _mockLogger.Object);
        //    controller.Post(_modelProducts.First());

        //    _mockProductRepository.Verify(p => p.Add(_entityProducts.First()), Times.Exactly(1));
        //    _mockUoW.Verify(p => p.Commit(), Times.Exactly(1));
        //}

        //[Test]
        //public void Post_WhenSavingValidProduct_ExceptionIsThrown()
        //{
        //    _mockMapper.Setup(x => x.Map<ProductEntity>(It.IsAny<Product>()))
        //        .Returns(_entityProducts.First());

        //    _mockUoW.Setup(p => p.ProductRepository).Returns(_mockProductRepository.Object);
        //    _mockProductRepository.Setup(p => p.Add(_entityProducts.First())).Throws(new Exception());

        //    ProductsController controller = new ProductsController(_mockUoW.Object, _mockMapper.Object, _mockLogger.Object);
        //    controller.Post(_modelProducts.First());

        //    _mockProductRepository.Verify(p => p.Add(_entityProducts.First()), Times.Exactly(1));
        //    _mockUoW.Verify(p => p.Commit(), Times.Exactly(0));
        //}

        //[Test]
        //public void Get_WhenGetProductById_ProductIsReturned()
        //{
        //    _mockMapper.Setup(x => x.Map<Product>(It.IsAny<ProductEntity>()))
        //        .Returns(_modelProducts.First());

        //    _mockUoW.Setup(p => p.ProductRepository).Returns(_mockProductRepository.Object);
        //    _mockProductRepository.Setup(p => p.Get(It.IsAny<Guid>())).Returns(_entityProducts.First());

        //    ProductsController controller = new ProductsController(_mockUoW.Object, _mockMapper.Object, _mockLogger.Object);
        //    var result = controller.Get(Guid.NewGuid());

        //    _mockProductRepository.Verify(p => p.Get(It.IsAny<Guid>()), Times.Exactly(1));
        //    Assert.AreEqual(_modelProducts.First().Id, result.Id);
        //}




        //[Test]
        //public void Get_WhenUpdatingProduct_ProductIsUpdated()
        //{
        //    _mockMapper.Setup(x => x.Map<ProductEntity>(It.IsAny<Product>()))
        //        .Returns(_entityProducts.First());

        //    _mockUoW.Setup(p => p.ProductRepository).Returns(_mockProductRepository.Object);
        //    _mockProductRepository.Setup(p => p.Update(_entityProducts.First())).Verifiable();
        //    _mockProductRepository.Setup(p => p.Get(It.IsAny<Guid>())).Returns(_entityProducts.First());

        //    ProductsController controller = new ProductsController(_mockUoW.Object, _mockMapper.Object, _mockLogger.Object);
        //    controller.Update(_entityProducts.First().Id, _modelProducts.First());

        //    _mockProductRepository.Verify(p => p.Update(_entityProducts.First()), Times.Exactly(1));
        //    _mockUoW.Verify(p => p.Commit(), Times.Exactly(1));
        //}

        //[Test]
        //public void Get_WhenDeletingProduct_ProductIsDeleted()
        //{
        //    _mockMapper.Setup(x => x.Map<ProductEntity>(It.IsAny<Product>()))
        //        .Returns(_entityProducts.First());

        //    _mockUoW.Setup(p => p.ProductRepository).Returns(_mockProductRepository.Object);
        //    _mockProductRepository.Setup(p => p.Delete(_entityProducts.First())).Verifiable();
        //    _mockProductRepository.Setup(p => p.Get(It.IsAny<Guid>())).Returns(_entityProducts.First());

        //    ProductsController controller = new ProductsController(_mockUoW.Object, _mockMapper.Object, _mockLogger.Object);
        //    controller.Delete(_entityProducts.First().Id);

        //    _mockProductRepository.Verify(p => p.Delete(_entityProducts.First()), Times.Exactly(1));
        //    _mockUoW.Verify(p => p.Commit(), Times.Exactly(1));
        //}

        //[Test]
        //public void Get_WhenGettingAllProductOptions_AllProductOptionsAreReturned()
        //{
        //    _mockMapper.Setup(x => x.Map<IEnumerable<ProductOption>>(It.IsAny<IEnumerable<ProductOptionEntity>>()))
        //        .Returns(_modelProductOptions);

        //    _mockProductOptionRepository.Setup(p => p.GetAll()).Returns(_entityProductOptions);

        //    _mockUoW.Setup(p => p.ProductOptionRepository).Returns(_mockProductOptionRepository.Object);

        //    ProductsController controller = new ProductsController(_mockUoW.Object, _mockMapper.Object, _mockLogger.Object);

        //    var result = controller.GetOptions(_modelProducts.First().Id);

        //    Assert.AreEqual(_modelProductOptions.Count, result.ToList().Count());
        //}

        //[Test]
        //public void Post_WhenSavingValidProductOption_ProductOptionIsSaved()
        //{
        //    _mockMapper.Setup(x => x.Map<ProductOptionEntity>(It.IsAny<ProductOption>()))
        //        .Returns(_entityProductOptions.First());

        //    _mockUoW.Setup(p => p.ProductOptionRepository).Returns(_mockProductOptionRepository.Object);
        //    _mockProductOptionRepository.Setup(p => p.Add(_entityProductOptions.First())).Verifiable();

        //    ProductsController controller = new ProductsController(_mockUoW.Object, _mockMapper.Object, _mockLogger.Object);
        //    controller.CreateOption(Guid.NewGuid(), _modelProductOptions.First());

        //    _mockProductOptionRepository.Verify(p => p.Add(_entityProductOptions.First()), Times.Exactly(1));
        //    _mockUoW.Verify(p => p.Commit(), Times.Exactly(1));
        //}

        //[Test]
        //public void Get_WhenGetProductOptionById_ProductOptionIsReturned()
        //{
        //    _mockMapper.Setup(x => x.Map<ProductOption>(It.IsAny<ProductOptionEntity>()))
        //        .Returns(_modelProductOptions.First());

        //    _mockUoW.Setup(p => p.ProductOptionRepository).Returns(_mockProductOptionRepository.Object);
        //    _mockProductOptionRepository.Setup(p => p.Get(It.IsAny<Guid>())).Returns(_entityProductOptions.First());

        //    ProductsController controller = new ProductsController(_mockUoW.Object, _mockMapper.Object, _mockLogger.Object);
        //    var result = controller.GetOption(Guid.NewGuid());

        //    _mockProductOptionRepository.Verify(p => p.Get(It.IsAny<Guid>()), Times.Exactly(1));
        //    Assert.AreEqual(_modelProductOptions.First().Id, result.Id);
        //}

        //[Test]
        //public void Get_WhenUpdatingProductOption_ProductOptionIsUpdated()
        //{
        //    _mockMapper.Setup(x => x.Map<ProductOptionEntity>(It.IsAny<ProductOption>()))
        //        .Returns(_entityProductOptions.First());

        //    _mockUoW.Setup(p => p.ProductOptionRepository).Returns(_mockProductOptionRepository.Object);
        //    _mockProductOptionRepository.Setup(p => p.Update(_entityProductOptions.First())).Verifiable();
        //    _mockProductOptionRepository.Setup(p => p.Get(It.IsAny<Guid>())).Returns(_entityProductOptions.First());

        //    ProductsController controller = new ProductsController(_mockUoW.Object, _mockMapper.Object, _mockLogger.Object);
        //    controller.UpdateOption(_entityProductOptions.First().Id, _modelProductOptions.First());

        //    _mockProductOptionRepository.Verify(p => p.Update(_entityProductOptions.First()), Times.Exactly(1));
        //    _mockUoW.Verify(p => p.Commit(), Times.Exactly(1));
        //}

        //[Test]
        //public void Get_WhenDeletingProductOption_ProductOptionIsDeleted()
        //{
        //    _mockMapper.Setup(x => x.Map<ProductOptionEntity>(It.IsAny<ProductOption>()))
        //        .Returns(_entityProductOptions.First());

        //    _mockUoW.Setup(p => p.ProductOptionRepository).Returns(_mockProductOptionRepository.Object);
        //    _mockProductOptionRepository.Setup(p => p.Delete(_entityProductOptions.First())).Verifiable();
        //    _mockProductOptionRepository.Setup(p => p.Get(It.IsAny<Guid>())).Returns(_entityProductOptions.First());

        //    ProductsController controller = new ProductsController(_mockUoW.Object, _mockMapper.Object, _mockLogger.Object);
        //    controller.DeleteOption(_entityProductOptions.First().Id);

        //    _mockProductOptionRepository.Verify(p => p.Delete(_entityProductOptions.First()), Times.Exactly(1));
        //    _mockUoW.Verify(p => p.Commit(), Times.Exactly(1));
        //}
    }
}