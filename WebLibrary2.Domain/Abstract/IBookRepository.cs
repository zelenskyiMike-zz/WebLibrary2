using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Models;

namespace WebLibrary2.Domain.Abstract
{
    public interface IBookRepository
    {
        IEnumerable<Book> Books { get; }
        Book GetBookByID(int? bookID);
        void InsertBook(AddABookViewModel book);
        void UpdateBook(Book book);
        void DeleteBook(int? bookID);
        int GetAddedID();
        void SaveBook();

    }
}
