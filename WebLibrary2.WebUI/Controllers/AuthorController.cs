using System.Linq;
using System.Web.Mvc;
using WebLibrary2.BusinessLogicLayer.Sevices;

namespace WebLibrary2.WebUI.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AuthorService service;

        public AuthorController(AuthorService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult AuthorView()
        {
            var author = service.GetAllAuthors().ToList();
            return View(author);
        }
    }
}