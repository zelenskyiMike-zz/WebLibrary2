using System.Collections.Generic;
using System.Linq;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Entity.BookEntity;
using WebLibrary2.Domain.Models;

namespace WebLibrary2.Domain.Abstract.AbstractBook
{
    public interface IBookRepository
    {
        IEnumerable<Book> Books { get; }
        Book GetBookByID(int? bookID);

        void InsertBook(BookViewModel bookVM);
        void UpdateBook(Book book);
        void DeleteBook(int? bookID);
        void SaveBook();

        GetM2MCRUDBookVM GetBooksDetails(int? id);
        List<Author> GetAuthorsNotExistInBook(int bookID);

        IQueryable<Book> GetAllBooksWithGenres();
    }
}
