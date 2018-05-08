using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.DataAccessLayer.Interfaces;

namespace WebLibrary2.DataAccessLayer.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private DbContext context;
        private DbSet<TEntity> dbSet;
        public GenericRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<TEntity>();
        }

        public void Create(TEntity item)
        {
            dbSet.Add(item);
            context.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return dbSet.AsNoTracking().ToList();
        }

        public TEntity GetByID(int id)
        {
            return dbSet.Find(id);
        }

        public void Remove(TEntity item)
        {
            dbSet.Remove(item);
            context.SaveChanges();
        }

        public void Update(TEntity item)
        {
            context.Entry(item).State =  EntityState.Modified;
            context.SaveChanges();
        }
    }
}
