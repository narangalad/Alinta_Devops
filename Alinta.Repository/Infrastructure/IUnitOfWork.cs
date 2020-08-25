using System;
using System.Collections.Generic;
using System.Text;

namespace Alinta.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository CustomerRepository { get; }
        void Commit();
    }

}
