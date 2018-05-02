using System.Web.Mvc;
using WebLibrary2.Domain.Concrete;
using System.Net;
using System.Data;
using WebLibrary2.Domain.Models;
using WebLibrary2.Domain.Abstract.AbstractAuthor;
using WebLibrary2.BLL.Sevices;

namespace WebLibrary2.WebUI.Controllers.AuthorControllers
{
    public class CRUDAuthorController : Controller
    {
        private readonly AuthorService service;

        private EFDbContext context;

        public CRUDAuthorController(EFDbContext dataContext, AuthorService service)
        {
            this.service = service;
            this.context = dataContext;
        }
        [Authorize(Roles = "admin")]
        public ActionResult CreateAuthor()
        {
            MultiSelectList books = new MultiSelectList(context.Books, "BookID", "BookName");
            ViewData["Books"] = books;
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAuthor(AuthorView authorVM)
        {
            if (ModelState.IsValid)
            {
                service.CreateAuthor(authorVM);
                return RedirectToAction("Index", "Home");
            }

            MultiSelectList books = new MultiSelectList(context.Books, "BookID", "BookName");
            ViewData["Books"] = books;
            return View(authorVM);
        }

        [Authorize(Roles = "user")]
        public ActionResult AuthorsDetails(int id)
        {
            GetAuthorLiteratureVM authorVM = service.GetAuthorsDetails(id);
            return View(authorVM);
        }

        [Authorize(Roles = "admin")]
        public ActionResult EditAuthor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GetAuthorLiteratureVM authorVM = service.GetAuthorsDetails(id);
            if (authorVM == null)
            {
                return HttpNotFound();
            }

            return View(authorVM);
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteAuthor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GetAuthorLiteratureVM author = service.GetAuthorsDetails(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAuthor(int id)
        {
            try
            {
                service.DeleteAuthor(id);
            }
            catch (DataException)
            {
                return RedirectToAction("DeleteAuthor", new { id = id });
            }
            return RedirectToAction("AuthorView", "Author");
        }
    }
}