using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Abstract.AbstractMagazine;
using WebLibrary2.Domain.Entity.MagazineEntity;

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
    }
}
