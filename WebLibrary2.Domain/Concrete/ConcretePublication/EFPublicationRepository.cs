using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Abstract.AbstractPublication;
using WebLibrary2.Domain.Entity.PublicationEntity;

namespace WebLibrary2.Domain.Concrete.ConcretePublication
{
    public class EFPublicationRepository : IPublicationRepository
    {
        EFDbContext context;
        public EFPublicationRepository(EFDbContext contextParam)
        {
            context = contextParam;
        }
        public IQueryable<Publication> GetAllPublications()
        {
            return context.Publications.Include(pg => pg.PublicationGenres);
        }
    }
}
