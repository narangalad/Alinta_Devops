using Alinta.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace Alinta.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly AlintaContext _DbContext;
        private readonly DbSet<TEntity> _DbSet;

        public GenericRepository(AlintaContext context)
        {
            this._DbContext = context;
            this._DbSet = this._DbContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            this._DbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            this._DbSet.Remove(entity);
        }

        public TEntity Get(long id)
        {
            return this._DbSet.Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this._DbSet.ToList();
        }

        public IEnumerable<TEntity> Search(string name)
        {
            return this._DbSet.ToList();
        }

        public void Update(TEntity entity)
        {
            this._DbSet.Attach(entity);
            this._DbContext.Entry(entity).State = EntityState.Modified;

        }
    }
}
