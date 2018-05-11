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
    public class PublicationService
    {
        private readonly DbContext context;
        private readonly GenericRepository<Publication> genericRepository;
        private readonly PublicationRepository publicationRepository;
        private readonly PublicationAuthorsRepository publicationAuthorsRepository;

        public PublicationService(DbContext context)
        {
            this.context = context;
            genericRepository = new GenericRepository<Publication>(context);
            publicationRepository = new PublicationRepository(context);
            publicationAuthorsRepository = new PublicationAuthorsRepository(context);
        }

        public IEnumerable<GetPublicationView> GetAllPubications()
        {
            var publications = genericRepository.GetAll().ToList();
            var publicationsMapped = Mapper.Map<IEnumerable<Publication>,IEnumerable< GetPublicationView>> (publications);
            return publicationsMapped;
        }
        public GetPublicationView GetPublicationByID(int id)
        {
            var publication = genericRepository.GetByID(id);
            var publicationMapped = Mapper.Map<Publication,GetPublicationView>(publication);
            return publicationMapped;
        }
        public GetAllPublicationsView GetPublicationDetails(int id)
        {
            var publications = publicationRepository.GetPublicationDetails(id);
            var publicationsMapped = Mapper.Map<Publication,GetAllPublicationsView>(publications);
            return publicationsMapped;
        }
        public IEnumerable<GetAuthorView> GetAuthorsNotExistInPublication(GetAllPublicationsView publicationVM)
        {
            var publication = Mapper.Map<GetAllPublicationsView,Publication>(publicationVM);
            var authors = publicationRepository.GetAuthorsNotExistInPublication(publication);
            var authorsMapped = Mapper.Map<IEnumerable<Author>, IEnumerable<GetAuthorView>>(authors);
            return authorsMapped;
        }
        public IEnumerable<GetPublicationGenreView> GetAllPublicationGenres()
        {
            var genres = context.PublicationGenres.ToList();
            var genresMapped = Mapper.Map<IEnumerable<PublicationGenre>,IEnumerable<GetPublicationGenreView>>(genres);
            return genresMapped;
        }

        public void CreatePublication(GetPublicationView publicationVM)
        {
            var publication = Mapper.Map<GetPublicationView,Publication>(publicationVM);
            genericRepository.Create(publication);
        }
        public void EditPublication(GetPublicationView publicationVM, int[] authorIDsForDelete, int[] authorIDsForInsert)
        {
            var publicationMapped = Mapper.Map<GetPublicationView,Publication>(publicationVM);
            genericRepository.Update(publicationMapped);
            publicationAuthorsRepository.DeleteAuthorFromPublication(publicationVM.PublicationID, authorIDsForDelete);
            publicationAuthorsRepository.AddAuthorToPublication(publicationVM.PublicationID, authorIDsForInsert);
            publicationRepository.Save();
        }
        public void DeletePublication(GetAllPublicationsView publicationVM)
        {
            var publicationMapped = Mapper.Map<GetAllPublicationsView,Publication>(publicationVM);
            genericRepository.Remove(publicationMapped);
        }
    }
}
