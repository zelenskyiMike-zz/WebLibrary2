using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Abstract.AbstractMagazine;
using WebLibrary2.Domain.Entity.MagazineEntity;

namespace WebLibrary2.Domain.Concrete.ConcreteMagazine
{
    public class EFMagazineAuthorRepository : IMagazineAuthorsRepository
    {
        EFDbContext context;
        public EFMagazineAuthorRepository(EFDbContext contextParam)
        {
            context = contextParam;
        }

        public void AddAuthorToMagazine(int magazineID, int[] authorIDsForInsert)
        {
            if (authorIDsForInsert != null)
            {
                foreach (var authorID in authorIDsForInsert)
                {
                    MagazineAuthor magazineToAdd = new MagazineAuthor()
                    {
                        MagazineID = magazineID,
                        AuthorID = authorID
                    };
                    context.SaveChanges();
                }
            }
        }

        public void AddMagazineToAuthor(int authorID, int[] magazineIDsForInsert)
        {
            if (magazineIDsForInsert != null)
            {
                foreach (var magazineID in magazineIDsForInsert)
                {
                    MagazineAuthor magazineToAdd = new MagazineAuthor()
                    {
                        MagazineID = magazineID,
                        AuthorID = authorID
                    };
                    context.SaveChanges();
                }
            }
        }

        public void DeleteAuthorFromMagazine(int magazineID, int[] authorIDsForDelete)
        {
            if (authorIDsForDelete != null)
            {
                foreach (var authorID in authorIDsForDelete)
                {
                    var magazineToRemove = context.MagazineAuthors.Find(magazineID, authorID);
                    context.MagazineAuthors.Remove(magazineToRemove);
                    context.SaveChanges();
                }
            }
        }

        public void DeleteMagazineFromAuthor(int authorID, int[] magazineIDsForDelete)
        {
            if (magazineIDsForDelete != null)
            {
                foreach (var magazineID in magazineIDsForDelete)
                {
                    var magazineToRemove = context.MagazineAuthors.Find(magazineID, authorID);
                    context.MagazineAuthors.Remove(magazineToRemove);
                    context.SaveChanges();
                }
            }
        }
    }
}
