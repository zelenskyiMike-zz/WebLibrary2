using System;
using System.Collections.Generic;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Models;

namespace WebLibrary2.Domain.Abstract
{
    public interface IAuthorsRepository
    {
        IEnumerable<Author> Authors { get; }

        //void makeJson(List<Author> author);
        void CreateAuthor(Author author);
        Author GetAuthorByID(int? id);
        void DeleteAuthor(Author author);
        void Save();
        GetM2MCRUDAuthorVM GetBookDetails(int? id);

    }
}
