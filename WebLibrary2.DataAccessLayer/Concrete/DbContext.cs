using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebLibrary2.DataAccessLayer.Identity;
using WebLibrary2.EntitiesLayer.Entities;

namespace WebLibrary2.DataAccessLayer.Concrete
{
    public class DbContext : IdentityDbContext<ApplicationUser>
    {
        public DbContext() : base("EFDbContext") { }
        public DbContext(string connectionString) : base("EFDbContext")
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

        public DbSet<ClientProfile> ClientProfiles { get; set; }
    }
}
