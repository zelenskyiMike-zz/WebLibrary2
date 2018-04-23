using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Abstract.AbstractArticle;
using WebLibrary2.Domain.Abstract.AbstractAuthor;
using WebLibrary2.Domain.Abstract.AbstractBook;
using WebLibrary2.Domain.Abstract.AbstractMagazine;
using WebLibrary2.Domain.Abstract.AbstractPublication;
using WebLibrary2.Domain.Entity;


namespace WebLibrary2.Domain.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        //IRepository<Book> Books { get; set; }
        //IRepository<Article> Articles { get; set; }
        //IRepository<Magazine> Magazines { get; set; }
        //IRepository<Publication> Publications { get; set; }
        //IRepository<Author> Authors { get; set; }

        IArticleRepository ArticleRepository { get; set; }
        IBookRepository BookRepository { get; set; }
        IMagazineRepository MagazineRepository { get; set; }
        IPublicationRepository PublicationRepository { get; set; }

        IAuthorsRepository AuthorsRepository { get; set; }

        void Save();
    }
}
