using System.Data.Entity;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Entity.PublicationEntity;
using WebLibrary2.Domain.Entity.MagazineEntity;
using WebLibrary2.Domain.Entity.BookEntity;
using WebLibrary2.Domain.Entity.ArticleEntity;

namespace WebLibrary2.Domain.Concrete
{
     public class EFDbContext : DbContext
    {
        public EFDbContext()
        {
        }
        
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookGenre> Genres { get; set; }
        public DbSet<Publication> Publicationes { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<ArticleAuthor> ArticleAuthors { get; set; }
        public DbSet<MagazineAuthor> MagazineAuthors { get; set; }
        public DbSet<PublicationeAuthor> PublicationeAuthors { get; set; }



        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        // //   modelBuilder.Entity<AuthorBook>().HasKey(ab => new { ab.BookID, ab.AuthorID });

        //    //base.OnModelCreating(modelBuilder);
        //}

    }
}
