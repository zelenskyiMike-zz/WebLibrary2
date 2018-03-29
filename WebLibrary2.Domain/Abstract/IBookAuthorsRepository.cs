using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.Domain.Abstract
{
    public interface IBookAuthorsRepository
    {
        void DeleteBookFromAuthor(int authorID, int [] bookIDs);
        void DeleteAuthorFromBook(int bookID, int[] authorIDs);
    }
}
