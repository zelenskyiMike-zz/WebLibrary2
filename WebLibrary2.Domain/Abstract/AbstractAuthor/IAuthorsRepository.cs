using System;
using System.Collections.Generic;
using System.Linq;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Entity.ArticleEntity;
using WebLibrary2.Domain.Entity.BookEntity;
using WebLibrary2.Domain.Entity.MagazineEntity;
using WebLibrary2.Domain.Entity.PublicationEntity;
using WebLibrary2.Domain.Models;

namespace WebLibrary2.Domain.Abstract.AbstractAuthor
{
    public interface IAuthorsRepository
    {
        IEnumerable<Author> Authors { get; }

        void CreateAuthor(AuthorViewModel authorVM);
        Author GetAuthorByID(int? id);
        void DeleteAuthor(int? id);
        void Save();

        GetM2MCRUDAuthorVM GetAuthorsDetails(int? id);
        GetM2MCRUDAuthorVM GetAuthorsBooksDetails(int? id);
        GetM2MCRUDAuthorVM GetAuthorsArticlesDetails(int? id);
        GetM2MCRUDAuthorVM GetAuthorsMagazinesDetails(int? id);
        GetM2MCRUDAuthorVM GetAuthorsPublicationsDetails(int? id);

        List<Book> GetBooksNotExistInAuthor(int authorID);
        List<Article> GetArticlesNotExistInAuthor(int authorID);
        List<Magazine> GetMagazinesNotExistInAuthor(int authorID);
        List<Publication> GetPublicationsNotExistInAuthor(int authorID);
        IQueryable<Author> GetAllAuthors();
    }
}
