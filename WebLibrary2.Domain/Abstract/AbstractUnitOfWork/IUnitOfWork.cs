using System;
using WebLibrary2.Domain.Abstract.AbstractArticle;
using WebLibrary2.Domain.Abstract.AbstractAuthor;
using WebLibrary2.Domain.Abstract.AbstractBook;
using WebLibrary2.Domain.Abstract.AbstractMagazine;
using WebLibrary2.Domain.Abstract.AbstractPublication;

namespace WebLibrary2.Domain.Abstract.AbstractUnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {

        IArticleRepository ArticleRepository { get;  }
        //IBookRepository BookRepository { get; }
        //IMagazineRepository MagazineRepository { get;}
        //IPublicationRepository PublicationRepository { get;}

        IAuthorsRepository AuthorsRepository { get;  }

        IArticeAuthorsRepository ArticeAuthorsRepository { get; }
        IBookAuthorsRepository BookAuthorsRepository { get;  }
        IMagazineAuthorsRepository MagazineAuthorsRepository { get; }
        IPublicationAuthorsRepository PublicationAuthorsRepository { get;}

        void Save();
    }
}
