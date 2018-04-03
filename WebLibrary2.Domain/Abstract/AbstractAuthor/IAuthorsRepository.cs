using System;
using System.Collections.Generic;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Entity.BookEntity;
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
        GetM2MCRUDAuthorVM GetAuthorDetails(int? id);

        List<Book> GetBooksNotExistInAuthor(int authorID);
    }
}
