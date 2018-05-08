using WebLibrary2.DataAccessLayer.Interfaces;
using WebLibrary2.EntitiesLayer.Entities;

namespace WebLibrary2.DataAccessLayer.Concrete
{
    public class EFPublicationAuthorsRepository : IPublicationAuthorsRepository
    {
        DbContext context;
        public EFPublicationAuthorsRepository(DbContext contextParam)
        {
            context = contextParam;
        }
        public void AddAuthorToPublication(int publicationID, int[] authorIDsForInsert)
        {
            if (authorIDsForInsert != null)
            {
                foreach (var authorID in authorIDsForInsert)
                {
                    PublicationeAuthor publicationeToAdd = new PublicationeAuthor()
                    {
                        PublicationID = publicationID,
                        AuthorID = authorID
                    };
                    context.PublicationeAuthors.Add(publicationeToAdd);
                    context.SaveChanges();
                }
            }
        }

        public void AddPublicationToAuthor(int authorID, int[] publicationIDsForInsert)
        {
            if (publicationIDsForInsert != null)
            {
                foreach (var publicationID in publicationIDsForInsert)
                {
                    PublicationeAuthor publicationeToAdd = new PublicationeAuthor()
                    {
                        PublicationID = publicationID,
                        AuthorID = authorID
                    };
                    context.PublicationeAuthors.Add(publicationeToAdd);
                    context.SaveChanges();
                }
            }
        }

        public void DeleteAuthorFromPublication(int publicationID, int[] authorIDsForDelete)
        {
            if (authorIDsForDelete != null)
            {
                foreach (var authorID in authorIDsForDelete)
                {
                    var publicationToDelete = context.PublicationeAuthors.Find(publicationID, authorID);
                    context.PublicationeAuthors.Remove(publicationToDelete);
                    context.SaveChanges();
                }
            }
        }

        public void DeletePublicationFromAuthor(int authorID, int[] publicationIDsForDelete)
        {
            if (publicationIDsForDelete != null)
            {
                foreach (var publicationID in publicationIDsForDelete)
                {
                    var publicationToDelete = context.PublicationeAuthors.Find(publicationID, authorID);
                    context.PublicationeAuthors.Remove(publicationToDelete);
                    context.SaveChanges();
                }
            }
        }
    }
}
