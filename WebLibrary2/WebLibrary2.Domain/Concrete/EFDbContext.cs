using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WebLibrary2.Domain.Entity;

namespace WebLibrary2.Domain.Concrete
{
    class EFDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
    }
}
