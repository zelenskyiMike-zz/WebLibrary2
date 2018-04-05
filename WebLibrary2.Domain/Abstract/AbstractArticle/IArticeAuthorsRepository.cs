using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.Domain.Abstract.AbstractArticle
{
    public interface IArticeAuthorsRepository
    {
        void DeleteArticleFromAuthor(int authorID, int[] magazineIDsForDelete);
        void DeleteAuthorFromArticle(int magazineID, int[] authorIDsForDelete);
        void AddAuthorToArticle(int magazineID, int[] authorIDsForInsert);
        void AddArticleToAuthor(int authorID, int[] magazineIDsForInsert);
    }
}
