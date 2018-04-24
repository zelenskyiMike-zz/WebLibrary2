using System.Linq;
using System.Web.Mvc;
using WebLibrary2.BLL.Sevices;

namespace WebLibrary2.WebUI.Controllers.AuthorControllers
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
            var author = service.GetAuthorViews().ToList();
            return View(author);
        }
    }
}