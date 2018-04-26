using System.Data.Entity;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Entity.PublicationEntity;
using WebLibrary2.Domain.Entity.MagazineEntity;
using WebLibrary2.Domain.Entity.BookEntity;
using WebLibrary2.Domain.Entity.ArticleEntity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using WebLibrary2.Domain.IdentityEntities;

namespace WebLibrary2.Domain.Concrete
{
    public class EFDbContext : IdentityDbContext<ApplicationUser>
    {
        public EFDbContext() : base("EFDbContext") { }
        public EFDbContext(string connectionString) : base("EFDbContext")
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<Article> Articles { get; set; }

        public DbSet<BookGenre> Genres { get; set; }
        public DbSet<ArticleGenre> ArticleGenres { get; set; }
        public DbSet<MagazineGenre> MagazineGenres { get; set; }
        public DbSet<PublicationGenre> PublicationGenres { get; set; }

        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<ArticleAuthor> ArticleAuthors { get; set; }
        public DbSet<MagazineAuthor> MagazineAuthors { get; set; }
        public DbSet<PublicationeAuthor> PublicationeAuthors { get; set; }
    }
}
