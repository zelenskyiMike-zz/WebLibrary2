using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using WebLibrary2.DataAccessLayer.Interfaces;
using WebLibrary2.EntitiesLayer.Entities;

namespace WebLibrary2.DataAccessLayer.Concrete
{
    public class PublicationRepository
    {
        DbContext context;
        public PublicationRepository(DbContext contextParam)
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

        public List<Author> GetAuthorsNotExistInPublication(Publication publication)
        {
           List<Author> finalListOfAuthors = new List<Author>();

            var initialListOfAuthors = context.PublicationeAuthors.Where(x => x.PublicationID == publication.PublicationID).Select(x => x.Authors).ToList();

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

        public Publication GetPublicationDetails(int? id)
        {
            Publication publication = GetPublicationByID(id);
            PublicationGenre publicationGenre =context.PublicationGenres.Where(x => x.PublicationGenreID == publication.PublicationGenreID).SingleOrDefault();
            var listAuthors = context.PublicationeAuthors.Where(x => x.PublicationID == publication.PublicationID).Select(x => x.Authors).ToList();

            Publication publicationVM = new Publication()
            {
                PublicationID = publication.PublicationID,
                PublicationName = publication.PublicationName,
                PublicationGenres= publicationGenre,
                DateOfPublicationPublish = publication.DateOfPublicationPublish,
                Authors = listAuthors
            };

            return publicationVM;
        }

        public void InsertPublication(Publication publicationVM)
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
