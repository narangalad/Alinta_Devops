using Alinta.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alinta.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AlintaContext _DbContext;

        public CustomerRepository(AlintaContext context)
        {
            _DbContext = context;
        }

        public void Add(Customer entity)
        {
            var vehicalMakes = _DbContext.Customers.Where(v => v.ID == entity.ID);
            _DbContext.Customers.Add(entity);
        }

        public void Delete(Customer entity)
        {
            _DbContext.Customers.Remove(entity);
        }

        public Customer Get(long id)
        {
            return (from customers in _DbContext.Customers
                    select new Customer
                    {
                        ID = customers.ID,
                        FirstName = customers.FirstName,
                        LastName = customers.LastName,
                        DateOfBirth = customers.DateOfBirth
                       
                    }).Where(a => a.ID == id).FirstOrDefault();
        }

        public IEnumerable<Customer> GetAll()
        {
            return from customer in _DbContext.Customers
                   select new Customer
                   {
                       ID = customer.ID,
                       FirstName = customer.FirstName,
                       LastName = customer.LastName,
                       DateOfBirth = customer.DateOfBirth,
                   };
        }

        public IEnumerable<Customer> Search(string name)
        {
            return (from customers in _DbContext.Customers
                    select new Customer
                    {
                        ID = customers.ID,
                        FirstName = customers.FirstName,
                        LastName = customers.LastName,
                        DateOfBirth = customers.DateOfBirth

                    }).Where(a => a.FirstName.Contains(name) || a.LastName.Contains(name));
        }

        public void Update(Customer entity)
        {
               _DbContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
