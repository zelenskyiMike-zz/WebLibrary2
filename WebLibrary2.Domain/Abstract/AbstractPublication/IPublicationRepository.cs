using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Entity.PublicationEntity;

namespace WebLibrary2.Domain.Abstract.AbstractPublication
{
    public interface IPublicationRepository
    {
        IQueryable<Publication> GetAllPublications();
    }
}
