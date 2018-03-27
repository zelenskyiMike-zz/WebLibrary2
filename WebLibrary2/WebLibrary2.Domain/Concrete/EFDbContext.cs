using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WebLibrary2.Domain.Entity;

namespace WebLibrary2.Domain.Concrete
{
     public class EFDbContext : DbContext
    {
        public EFDbContext()
        {
            Database.SetInitializer<EFDbContext>(null);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }

     }
}
