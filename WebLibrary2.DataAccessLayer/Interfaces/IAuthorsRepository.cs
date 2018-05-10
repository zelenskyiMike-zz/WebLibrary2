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

        Author GetAuthorsDetails(Author author);
        Author GetAuthorsBooksDetails(int? id);
        Author GetAuthorsArticlesDetails(int? id);
        Author GetAuthorsMagazinesDetails(int? id);
        Author GetAuthorsPublicationsDetails(int? id);

        List<Book> GetBooksNotExistInAuthor(Author author);
        List<Article> GetArticlesNotExistInAuthor(Author author);
        List<Magazine> GetMagazinesNotExistInAuthor(Author author);
        List<Publication> GetPublicationsNotExistInAuthor(Author author);
        IQueryable<Author> GetAllAuthors();
    }
}
