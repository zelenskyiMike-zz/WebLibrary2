using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.Domain.Abstract
{
    public interface IBookAuthorsRepository
    {
        void DeleteBookFromAuthor(int authorID, int [] bookIDsForDelete);
        void DeleteAuthorFromBook(int bookID, int[] authorIDsForDelete);
        void AddAuthorToBook(int bookID, int [] authorIDsForInsert);
        void AddBookToAuthor(int authorID, int[] bookIDsForInsert);
    }
}
