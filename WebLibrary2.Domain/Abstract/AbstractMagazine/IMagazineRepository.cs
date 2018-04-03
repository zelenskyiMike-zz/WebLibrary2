using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Entity.MagazineEntity;

namespace WebLibrary2.Domain.Abstract.AbstractMagazine
{
    public interface IMagazineRepository
    {
        IQueryable<Magazine> GetAllMagazines();
    }
}
