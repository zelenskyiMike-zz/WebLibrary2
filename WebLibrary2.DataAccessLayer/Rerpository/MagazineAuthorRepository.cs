using WebLibrary2.DataAccessLayer.Interfaces;
using WebLibrary2.EntitiesLayer.Entities;

namespace WebLibrary2.DataAccessLayer.Concrete
{
    public class MagazineAuthorRepository
    {
        DbContext context;
        public MagazineAuthorRepository(DbContext contextParam)
        {
            context = contextParam;
        }

        public void AddAuthorToMagazine(int magazineID, int[] authorIDsForInsert)
        {
            if (authorIDsForInsert == null)
            {
                return;
            }
            foreach (var authorID in authorIDsForInsert)
            {
                MagazineAuthor magazineToAdd = new MagazineAuthor()
                {
                    MagazineID = magazineID,
                    AuthorID = authorID
                };
                context.MagazineAuthors.Add(magazineToAdd);
                context.SaveChanges();
            }

        }

        public void AddMagazineToAuthor(int authorID, int[] magazineIDsForInsert)
        {
            if (magazineIDsForInsert == null)
            {
                return;
            }
            foreach (var magazineID in magazineIDsForInsert)
            {
                MagazineAuthor magazineToAdd = new MagazineAuthor()
                {
                    MagazineID = magazineID,
                    AuthorID = authorID
                };
                context.MagazineAuthors.Add(magazineToAdd);
                context.SaveChanges();
            }

        }

        public void DeleteAuthorFromMagazine(int magazineID, int[] authorIDsForDelete)
        {
            if (authorIDsForDelete == null)
            {
                return;
            }
            foreach (var authorID in authorIDsForDelete)
            {
                var magazineToRemove = context.MagazineAuthors.Find(magazineID, authorID);
                context.MagazineAuthors.Remove(magazineToRemove);
                context.SaveChanges();
            }

        }

        public void DeleteMagazineFromAuthor(int authorID, int[] magazineIDsForDelete)
        {
            if (magazineIDsForDelete == null)
            {
                return;
            }
            foreach (var magazineID in magazineIDsForDelete)
            {
                var magazineToRemove = context.MagazineAuthors.Find(magazineID, authorID);
                context.MagazineAuthors.Remove(magazineToRemove);
                context.SaveChanges();
            }
        }

    }
}
