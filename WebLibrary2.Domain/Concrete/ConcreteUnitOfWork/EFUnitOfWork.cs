using WebLibrary2.Domain.Abstract.AbstractArticle;
using WebLibrary2.Domain.Abstract.AbstractAuthor;
using WebLibrary2.Domain.Abstract.AbstractBook;
using WebLibrary2.Domain.Abstract.AbstractMagazine;
using WebLibrary2.Domain.Abstract.AbstractPublication;
using WebLibrary2.Domain.Abstract.AbstractUnitOfWork;
using WebLibrary2.Domain.Concrete.ConcreteArticle;
using WebLibrary2.Domain.Concrete.ConcreteAuthor;
using WebLibrary2.Domain.Concrete.ConcreteBook;
using WebLibrary2.Domain.Concrete.ConcreteMagazine;
using WebLibrary2.Domain.Concrete.ConcretePublication;
namespace WebLibrary2.Domain.Concrete.ConcreteUnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private EFDbContext dbContext;
        private EFArticleRepository articleRepository;
        private EFArticleAuthorsRepository articleAuthorsRepository;
        private EFAuthorRepository authorRepository;
        private EFBookRepository bookRepository;
        private EFBookAuthorRepository bookAuthorRepository;
        private EFMagazineRepository magazineRepository;
        private EFMagazineAuthorRepository magazineAuthorRepository;
        private EFPublicationRepository publicationRepository;
        private EFPublicationAuthorsRepository publicationAuthorsRepository;

        private bool disposed = false;

        public EFUnitOfWork(string connectionString)
        {
            this.dbContext = new EFDbContext(connectionString);
        }

        public IArticleRepository ArticleRepository
        {
            get
            {
                if (articleRepository == null)
                {
                    articleRepository = new EFArticleRepository(dbContext);
                }
                return articleRepository;
            }
        }
        public IBookRepository BookRepository
        {
            get
            {
                if (bookRepository == null)
                {
                    bookRepository = new EFBookRepository(dbContext);
                }
                return bookRepository;
            }
        }
        public IMagazineRepository MagazineRepository
        {
            get
            {
                if (magazineRepository == null)
                {
                    magazineRepository = new EFMagazineRepository(dbContext);
                }
                return magazineRepository;
            }
        }
        public IPublicationRepository PublicationRepository
        {
            get
            {
                if (publicationRepository == null)
                {
                    publicationRepository = new EFPublicationRepository(dbContext);
                }
                return publicationRepository;
            }
        }
        public IAuthorsRepository AuthorsRepository
        {
            get
            {
                if (authorRepository == null)
                {
                    authorRepository = new EFAuthorRepository(dbContext);
                }
                return authorRepository;
            }
        }
        public IArticeAuthorsRepository ArticeAuthorsRepository
        {
            get
            {
                if (articleAuthorsRepository == null)
                {
                    articleAuthorsRepository = new EFArticleAuthorsRepository(dbContext);
                }
                return articleAuthorsRepository;
            }
        }
        public IBookAuthorsRepository BookAuthorsRepository
        {
            get
            {
                if (bookAuthorRepository == null)
                {
                    bookAuthorRepository = new EFBookAuthorRepository(dbContext);
                }
                return bookAuthorRepository;
            }
        }
        public IMagazineAuthorsRepository MagazineAuthorsRepository
        {
            get
            {
                if (magazineAuthorRepository == null)
                {
                    magazineAuthorRepository = new EFMagazineAuthorRepository(dbContext);
                }
                return magazineAuthorRepository;
            }
        }
        public IPublicationAuthorsRepository PublicationAuthorsRepository
        {
            get
            {
                if (publicationAuthorsRepository == null)
                {
                    publicationAuthorsRepository = new EFPublicationAuthorsRepository(dbContext);
                }
                return publicationAuthorsRepository;
            }
        }


        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Save()
        {
            dbContext.SaveChanges();    
        }
    }
}
