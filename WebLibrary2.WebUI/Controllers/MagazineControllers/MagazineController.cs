using System.Web.Mvc;
using WebLibrary2.Domain.Abstract.AbstractMagazine;

namespace WebLibrary2.WebUI.Controllers.MagazineControllers
{
    public class MagazineController : Controller
    {
        IMagazineRepository magazineRepository;
        public MagazineController(IMagazineRepository magazinesRepository)
        {
            magazineRepository = magazinesRepository;
        }
        // GET: Magazine
        public PartialViewResult MagazinesView()
        {
            var magazines = magazineRepository.GetAllMagazines();
            return PartialView(magazines);
        }
    }
}