using System.Collections.Generic;
using System.Linq;
using WebLibrary2.EntitiesLayer.Entities;

namespace WebLibrary2.DataAccessLayer.Interfaces
{
    public interface IPublicationRepository
    {
        IQueryable<Publication> GetAllPublications();
        void InsertPublication(Publication publicationVM);

        Publication GetPublicationDetails(int? id);
        Publication GetPublicationByID(int? id);
        List<Author> GetAuthorsNotExistInPublication(int id);
        void DeletePublication(int id);
        void Save();
    }
}
