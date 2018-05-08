using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.DataAccessLayer.Interfaces
{
    public interface IMagazineAuthorsRepository
    {
        void DeleteMagazineFromAuthor(int authorID, int[] magazineIDsForDelete);
        void DeleteAuthorFromMagazine(int magazineID, int[] authorIDsForDelete);
        void AddAuthorToMagazine(int magazineID, int[] authorIDsForInsert);
        void AddMagazineToAuthor(int authorID, int[] magazineIDsForInsert);
    }
}
