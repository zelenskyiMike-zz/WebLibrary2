using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Abstract.AbstractPublication;
using WebLibrary2.Domain.Entity.PublicationEntity;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Models;

namespace WebLibrary2.Domain.Concrete.ConcretePublication
{
    public class EFPublicationRepository : IPublicationRepository
    {
        EFDbContext context;
        public EFPublicationRepository(EFDbContext contextParam)
        {
            context = contextParam;
        }

        public void DeletePublication(int id)
        {
            Publication publicationToDelete = GetPublicationByID(id);
            context.Publications.Remove(publicationToDelete);
            Save();
        }

        public IQueryable<Publication> GetAllPublications()
        {
            return context.Publications.Include(pg => pg.PublicationGenres);
        }

        public List<Author> GetAuthorsNotExistInPublication(int id)
        {
            Publication currPublication = GetPublicationByID(id);

            List<Author> finalListOfAuthors = new List<Author>();

            var initialListOfAuthors = context.PublicationeAuthors.Where(x => x.PublicationID == currPublication.PublicationID).Select(x => x.Authors).ToList();

            foreach (var item in context.Authors.ToList())
            {
                if (!initialListOfAuthors.Contains(item))
                {
                    finalListOfAuthors.Add(item);
                }
            }

            return finalListOfAuthors;
        }

        public Publication GetPublicationByID(int? id)
        {
            return context.Publications.Find(id);
        }

        public GetM2MCRUDPublicationVM GetPublicationDetails(int? id)
        {
            Publication publication = GetPublicationByID(id);
            var publicationGenreName = (from pg in context.PublicationGenres
                                        where pg.PublicationGenreID == publication.PublicationGenreID
                                        select pg.PublicationGenreName).SingleOrDefault();
            var listAuthors = context.PublicationeAuthors.Where(x => x.PublicationID == publication.PublicationID).Select(x => x.Authors).ToList();

            GetM2MCRUDPublicationVM publicationVM = new GetM2MCRUDPublicationVM()
            {
                PublicationID = publication.PublicationID,
                PublicationName = publication.PublicationName,
                PublicationGenreName = publicationGenreName,
                DateOfPublicationPublish = publication.DateOfPublicationPublish,
                Authors = listAuthors
            };

            return publicationVM;
        }

        public void InsertPublication(PublicationViewModel publicationVM)
        {
            Publication publication = new Publication()
            {
                PublicationName = publicationVM.PublicationName,
                PublicationGenreID = publicationVM.PublicationGenreID,
                DateOfPublicationPublish = publicationVM.DateOfPublicationPublish
            };
            context.Publications.Add(publication);
            Save();
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
