using Alinta.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alinta.Repository.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public AlintaContext _DbContext;
        public ICustomerRepository CustomerRepository { get; }

        public UnitOfWork(AlintaContext context, ICustomerRepository carRepository)
        {
            this._DbContext = context;
            this.CustomerRepository = carRepository;
        }

        public void Commit()
        {
            this._DbContext.SaveChanges();
        }

        public void Dispose()
        {
            this._DbContext.Dispose();
        }
    }
}
