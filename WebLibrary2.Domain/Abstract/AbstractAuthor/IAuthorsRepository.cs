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

        void CreateAuthor(AuthorView authorVM);
        Author GetAuthorByID(int? id);
        void DeleteAuthor(int? id);
        void Save();

        GetAuthorLiteratureVM GetAuthorsDetails(int? id);
        GetAuthorLiteratureVM GetAuthorsBooksDetails(int? id);
        GetAuthorLiteratureVM GetAuthorsArticlesDetails(int? id);
        GetAuthorLiteratureVM GetAuthorsMagazinesDetails(int? id);
        GetAuthorLiteratureVM GetAuthorsPublicationsDetails(int? id);

        List<Book> GetBooksNotExistInAuthor(int authorID);
        List<Article> GetArticlesNotExistInAuthor(int authorID);
        List<Magazine> GetMagazinesNotExistInAuthor(int authorID);
        List<Publication> GetPublicationsNotExistInAuthor(int authorID);
        IQueryable<Author> GetAllAuthors();
    }
}
