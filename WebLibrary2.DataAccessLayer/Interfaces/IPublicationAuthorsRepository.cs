using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary2.DataAccessLayer.Interfaces
{
    public interface IPublicationAuthorsRepository
    {
        void DeletePublicationFromAuthor(int authorID, int[] publicationIDsForDelete);
        void DeleteAuthorFromPublication(int publicationID, int[] authorIDsForDelete);
        void AddAuthorToPublication(int publicationID, int[] authorIDsForInsert);
        void AddPublicationToAuthor(int authorID, int[] publicationIDsForInsert);
    }
}
