using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebLibrary2.DataAccessLayer.Concrete;
using WebLibrary2.EntitiesLayer.Entities;
using WebLibrary2.ViewModelsLayer.ViewModels;

namespace WebLibrary2.BusinessLogicLayer.Sevices
{
    public class MagazineService
    {
        private readonly DbContext context;
        private readonly GenericRepository<Magazine> genericRepository;
        private readonly MagazineRepository magazineRepository;
        private readonly MagazineAuthorRepository magazineAuthorsRepository;

        public MagazineService(DbContext context)
        {
            this.context = context;
            genericRepository = new GenericRepository<Magazine>(context);
            magazineRepository = new MagazineRepository(context);
            magazineAuthorsRepository = new MagazineAuthorRepository(context);
        }

        public IEnumerable<GetAllMagazinesView> GetAllMagazinesWithGenres()
        {
            var magazines = magazineRepository.GetAllMagazinesWithGenres() ;
            var magazinesMapped = Mapper.Map<IEnumerable<Magazine>,IEnumerable<GetAllMagazinesView>>(magazines);
            return magazinesMapped;
        }
        public GetMagazineView GetMagazineByID(int id)
        {
            var magazine = genericRepository.GetByID(id);
            var magazineMapped = Mapper.Map<Magazine,GetMagazineView>(magazine);
            return magazineMapped;
        }
        public GetAllMagazinesView GetMagazineDetails(int id)
        {
            var magazine = genericRepository.GetByID(id);
            var magazineDetails = magazineRepository.GetMagazineDetails(magazine);
            var mappedMagazineDetails = Mapper.Map<Magazine, GetAllMagazinesView>(magazineDetails);
            return mappedMagazineDetails;
        }
        public void CreateMagazine(GetMagazineView magazineVM)
        {
            var magazine = Mapper.Map<GetMagazineView,Magazine>(magazineVM);
            genericRepository.Create(magazine);
        }
        public void EditMagazine(GetAllMagazinesView magazineVM, int[] authorIDsForDelete, int[] authorIDsForInsert)
        {
            var magazineToUpdate = Mapper.Map<GetAllMagazinesView,Magazine>(magazineVM);
            genericRepository.Update(magazineToUpdate);
            magazineAuthorsRepository.DeleteAuthorFromMagazine(magazineToUpdate.MagazineID, authorIDsForDelete);
            magazineAuthorsRepository.AddAuthorToMagazine(magazineToUpdate.MagazineID, authorIDsForInsert);
            magazineRepository.Save();
        }
        public void DeleteMagazine(int id)
        {
            var magazine = genericRepository.GetByID(id);
            genericRepository.Remove(magazine);
        }
        public IEnumerable<GetMagazineGenreView> GetAllGenres()
        {
            var genres = context.MagazineGenres.ToList();
            var genresMapped = Mapper.Map<IEnumerable<MagazineGenre>,IEnumerable<GetMagazineGenreView>>(genres);
            return genresMapped;
        }

        public IEnumerable<GetAuthorView> GetAuthorsNotExistInMagazine(GetAllMagazinesView magazineVM)
        {
            var magazine = Mapper.Map<GetAllMagazinesView,Magazine>(magazineVM);
            var authors = magazineRepository.GetAuthorsNotExistInMagazine(magazine);
            var authorsMapped = Mapper.Map<IEnumerable<Author>,IEnumerable<GetAuthorView>>(authors);
            return authorsMapped;
        }
    }
}
