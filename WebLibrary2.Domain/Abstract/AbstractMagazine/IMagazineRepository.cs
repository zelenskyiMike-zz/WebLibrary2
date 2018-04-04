using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.Domain.Entity;
using WebLibrary2.Domain.Entity.MagazineEntity;
using WebLibrary2.Domain.Models;

namespace WebLibrary2.Domain.Abstract.AbstractMagazine
{
    public interface IMagazineRepository
    {
        IQueryable<Magazine> GetAllMagazines();
        Magazine GetMagazineByID(int? id);
        
        void InsertMagazine(MagazineViewModel magazineVM);

        GetM2MCRUDMagazineVM GetMagazineDetails(int? id);
        List<Author> GetAuthorsNotExistInMagazine(int id);
    }
}
