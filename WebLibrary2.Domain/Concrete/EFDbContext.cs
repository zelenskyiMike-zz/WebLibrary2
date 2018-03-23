using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WebLibrary2.Domain.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebLibrary2.Domain.Concrete
{
     public class EFDbContext : DbContext
    {
        public EFDbContext()
        {
        }
        
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        // //   modelBuilder.Entity<AuthorBook>().HasKey(ab => new { ab.BookID, ab.AuthorID });

        //    //base.OnModelCreating(modelBuilder);
        //}

    }
}
