using System;
using System.Collections.Generic;
using System.Text;

namespace Alinta.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(long id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Search(string name);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

    }
}
