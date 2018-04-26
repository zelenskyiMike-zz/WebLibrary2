using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.Domain.Concrete
{
    public class ContextFactory : IDbContextFactory<EFDbContext>
    {
        public EFDbContext Create()
        {
            return new EFDbContext("EFDbContext");
        }
    }
}
