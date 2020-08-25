using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Alinta.Data.Entities;
using Microsoft.Extensions.Logging;
using Alinta.Repository;
using AutoMapper;
using Customer = Alinta.Api.Models.Customer; 
using CustomerEntities = Alinta.Data.Entities.Customer;

namespace Alinta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public CustomersController(IUnitOfWork unitOfWork,
                                       ILogger<CustomersController> logger,
                                       IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Customers
        [HttpGet]
        public IEnumerable<Customer> GetCustomers()
        {
            var customers = _unitOfWork.CustomerRepository.GetAll().ToList();

            return  _mapper.Map<IEnumerable<Customer>>(customers);

        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(long id)
        {
            var customers = _unitOfWork.CustomerRepository.Get(id);
            return _mapper.Map<Customer>(customers);
        }


        // GET: api/Customers/search?name=test
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Customer>>> Search(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var customers = _unitOfWork.CustomerRepository.Search(name).ToList();

                if (customers.Any())
                {
                  
                    return CreatedAtAction("Search", customers);
                }
                else
                {
                    return new NotFoundResult();
                }
            }

            return BadRequest();

        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(long id, Customer customer)
        {
            if (id != customer.ID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var entity = _unitOfWork.CustomerRepository.Get(id);

                if (entity != null)
                {
                  
                    try
                    {
                        _unitOfWork.CustomerRepository.Update(entity);
                        _unitOfWork.Commit();
                        return Ok();
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e.Message);
                        return StatusCode(500);
                    }
                }
            }

            return BadRequest();


        }

        // POST: api/Customers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            var entity = _mapper.Map<CustomerEntities>(customer);
            _unitOfWork.CustomerRepository.Add(entity);
            try
            {
                _unitOfWork.Commit();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCustomers", new { id = customer.ID }, customer);
            
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(long id)
        {
            var customers = _unitOfWork.CustomerRepository.Get(id);
            if (customers == null)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    var entity = _unitOfWork.CustomerRepository.Get(id);
                    _unitOfWork.CustomerRepository.Delete(entity);
                    _unitOfWork.Commit();
                    return Ok();
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                    return StatusCode(500);
                }
            }
            return BadRequest();
        }

        private bool CustomerExists(long id)
        {
            var result = _unitOfWork.CustomerRepository.Get(id);
            return false;
        }
    }
}
