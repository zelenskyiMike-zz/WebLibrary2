using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.DataAccessLayer.Concrete
{
    public class ContextFactory : IDbContextFactory<DbContext>
    {
        public DbContext Create()
        {
            return new DbContext("EFDbContext");
        }
    }
}
