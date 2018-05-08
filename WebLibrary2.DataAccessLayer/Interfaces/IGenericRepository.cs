using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.DataAccessLayer.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity: class
    {
        void Create(TEntity item);
        TEntity GetByID(int id);
        IEnumerable<TEntity> GetAll();
        void Remove(TEntity item);
        void Update(TEntity item);
    }
}
