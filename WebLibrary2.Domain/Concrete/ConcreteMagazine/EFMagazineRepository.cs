using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Abstract.AbstractMagazine;
using WebLibrary2.Domain.Entity.MagazineEntity;
using WebLibrary2.Domain.Models;
using WebLibrary2.Domain.Entity;

namespace WebLibrary2.Domain.Concrete.ConcreteMagazine
{
    public class EFMagazineRepository : IMagazineRepository
    {
        EFDbContext context;
        public EFMagazineRepository(EFDbContext contextParam)
        {
            context = contextParam;
        }

        public IQueryable<Magazine> GetAllMagazines()
        {
            return context.Magazines.Include(mg => mg.MagazineGenres);
        }

        public List<Author> GetAuthorsNotExistInMagazine(int id)
        {
            var currMagazine = GetMagazineByID(id);

            var initMagazineAuthorList = context.MagazineAuthors.Where(x => x.MagazineID == currMagazine.MagazineID).Select(x => x.Authors).ToList();

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

        public GetM2MCRUDMagazineVM GetMagazineDetails(int? id)
        {
            Magazine magazine = GetMagazineByID(id);
            var magazinegenreName = (from mg in context.MagazineGenres
                             where mg.MagazineGenreID == magazine.MagazineGenreID
                             select mg.MagazineGenreName).SingleOrDefault();
            var authorsList = context.MagazineAuthors.Include(x => x.Authors).Where(x => x.MagazineID == id).Select(x => x.Authors).ToList();

            GetM2MCRUDMagazineVM magazineVM = new GetM2MCRUDMagazineVM()
            {
                MagazineID = magazine.MagazineID,
                MagazineName = magazine.MagazineName,
                MagazineGenreName = magazinegenreName,
                Authors = authorsList

            };
            return magazineVM;
        }

        public void InsertMagazine(MagazineViewModel magazineVM)
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
    }
}
