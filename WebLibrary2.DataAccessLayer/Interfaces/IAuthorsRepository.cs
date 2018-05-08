using System.Collections.Generic;
using System.Linq;
using WebLibrary2.EntitiesLayer.Entities;

namespace WebLibrary2.DataAccessLayer.Interfaces
{
    public interface IAuthorsRepository
    {
        IEnumerable<Author> Authors { get; }

        void CreateAuthor(Author authorVM);
        Author GetAuthorByID(int? id);
        void DeleteAuthor(int? id);
        void Save();

        Author GetAuthorsDetails(int id);
        Author GetAuthorsBooksDetails(int? id);
        Author GetAuthorsArticlesDetails(int? id);
        Author GetAuthorsMagazinesDetails(int? id);
        Author GetAuthorsPublicationsDetails(int? id);

        List<Book> GetBooksNotExistInAuthor(int authorID);
        List<Article> GetArticlesNotExistInAuthor(int authorID);
        List<Magazine> GetMagazinesNotExistInAuthor(int authorID);
        List<Publication> GetPublicationsNotExistInAuthor(int authorID);
        IQueryable<Author> GetAllAuthors();
    }
}
