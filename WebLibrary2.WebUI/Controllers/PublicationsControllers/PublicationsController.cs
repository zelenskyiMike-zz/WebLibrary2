using System.Web.Mvc;
using WebLibrary2.Domain.Abstract.AbstractPublication;

namespace WebLibrary2.WebUI.Controllers.PublicationsControllers
{
    public class PublicationsController : Controller
    {
        IPublicationRepository publicationRepository;
        public PublicationsController(IPublicationRepository publicationsRepository)
        {
            publicationRepository = publicationsRepository;
        }
        public PartialViewResult PublicationsView()
        {
            var publications = publicationRepository.GetAllPublications();
            return PartialView(publications);
        }
    }
}