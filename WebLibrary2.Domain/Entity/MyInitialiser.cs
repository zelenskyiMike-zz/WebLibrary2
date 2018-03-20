using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WebLibrary2.Domain.Concrete;

namespace WebLibrary2.Domain.Entity
{
    public class MyInitialiser : CreateDatabaseIfNotExists<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            base.Seed(context);
        }
    }
}
