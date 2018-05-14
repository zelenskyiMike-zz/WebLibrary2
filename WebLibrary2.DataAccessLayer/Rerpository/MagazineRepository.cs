using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using WebLibrary2.DataAccessLayer.Interfaces;
using WebLibrary2.EntitiesLayer.Entities;

namespace WebLibrary2.DataAccessLayer.Concrete
{
    public class MagazineRepository
    {
        DbContext context;
        public MagazineRepository(DbContext contextParam)
        {
            context = contextParam;
        }

        public void DeleteMagazine(int? magazineID)
        {
            var magazineToDelete = GetMagazineByID(magazineID);
            context.Magazines.Remove(magazineToDelete);
            context.SaveChanges();
        }

        public IQueryable<Magazine> GetAllMagazinesWithGenres()
        {
            return context.Magazines.Include(mg => mg.MagazineGenres);
        }

        public List<Author> GetAuthorsNotExistInMagazine(Magazine magazine)
        {
            var initMagazineAuthorList = context.MagazineAuthors.Where(x => x.MagazineID == magazine.MagazineID).Select(x => x.Authors).ToList();

            List<Author> finalListOfAuthors = new List<Author>();

            foreach (var item in context.Authors.ToList())
            {
                if (!initMagazineAuthorList.Contains(item))
                {
                    finalListOfAuthors.Add(item);
                }
            }

            return finalListOfAuthors;
        }

        public Magazine GetMagazineByID(int? id)
        {
            return context.Magazines.Find(id);
        }

        public Magazine GetMagazineDetails(Magazine magazine)
        {
            MagazineGenre magazineGenre = context.MagazineGenres.Where(x => x.MagazineGenreID == magazine.MagazineGenreID).SingleOrDefault();
            var authorsList = context.MagazineAuthors.Include(x => x.Authors).Where(x => x.MagazineID == magazine.MagazineID).Select(x => x.Authors).ToList();

            Magazine magazineVM = new Magazine()
            {
                MagazineID = magazine.MagazineID,
                MagazineName = magazine.MagazineName,
                MagazineGenres = magazineGenre,
                DateOfMagazinePublish = magazine.DateOfMagazinePublish,
                Authors = authorsList

            };
            return magazineVM;
        }

        public void InsertMagazine(Magazine magazineVM)
        {
            Magazine magazine = new Magazine()
            {
                MagazineName = magazineVM.MagazineName,
                MagazineGenreID = magazineVM.MagazineGenreID,
                DateOfMagazinePublish = magazineVM.DateOfMagazinePublish
            };

            context.Magazines.Add(magazine);
            context.SaveChanges();

            foreach (var item in magazineVM.AuthorsIDs)
            {
                MagazineAuthor magazineAuthor = new MagazineAuthor()
                {
                    AuthorID = item,
                    MagazineID = magazine.MagazineID
                };
                context.MagazineAuthors.Add(magazineAuthor);
                context.SaveChanges();
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
