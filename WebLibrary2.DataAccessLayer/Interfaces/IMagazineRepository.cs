using System.Collections.Generic;
using System.Linq;
using WebLibrary2.EntitiesLayer.Entities;

namespace WebLibrary2.DataAccessLayer.Interfaces
{
    public interface IMagazineRepository
    {
        IQueryable<Magazine> GetAllMagazines();
        Magazine GetMagazineByID(int? id);
        
        void InsertMagazine(Magazine magazineVM);
        void DeleteMagazine(int? magazineID);
        void Save();

        Magazine GetMagazineDetails(int? id);
        List<Author> GetAuthorsNotExistInMagazine(int id);
    }
}
