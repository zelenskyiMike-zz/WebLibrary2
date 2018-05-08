using System;
using WebLibrary2.DataAccessLayer.Interfaces;

namespace WebLibrary2.DataAccessLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {

        IArticleRepository ArticleRepository { get;  }

        IAuthorsRepository AuthorsRepository { get;  }
        IBookRepository BookRepository { get; }

        IArticeAuthorsRepository ArticeAuthorsRepository { get; }
        IBookAuthorsRepository BookAuthorsRepository { get;  }
        IMagazineAuthorsRepository MagazineAuthorsRepository { get; }
        IPublicationAuthorsRepository PublicationAuthorsRepository { get;}

        void Save();
    }
}
