using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Entity.PublicationEntity;
using WebLibrary2.Domain.Models;

namespace WebLibrary2.Domain.Abstract.AbstractPublication
{
    public interface IPublicationRepository
    {
        IQueryable<Publication> GetAllPublications();
        void InsertPublication(PublicationViewModel publicationVM);

        GetM2MCRUDPublicationVM GetPublicationDetails(int? id);
        Publication GetPublicationByID(int? id);
        List<Author> GetAuthorsNotExistInPublication(int id);
        void DeletePublication(int id);
        void Save();
    }
}
