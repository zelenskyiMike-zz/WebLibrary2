using System.Web.Mvc;
using System.Net;
using System.Data;
using WebLibrary2.ViewModelsLayer.ViewModels;
using WebLibrary2.DataAccessLayer.Concrete;
using WebLibrary2.BusinessLogicLayer.Sevices;

namespace WebLibrary2.WebUI.Controllers.AuthorControllers
{
    public class CRUDAuthorController : Controller
    {
        private readonly AuthorService service;

        private DbContext context;

        public CRUDAuthorController(DbContext dataContext, AuthorService service)
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
        public ActionResult CreateAuthor(GetAuthorView authorVM)
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

        [Authorize(Roles = "user,admin")]
        public ActionResult AuthorsDetails(int id)
        {
            GetAuthorLiteratureView authorVM = service.GetAuthor(id);
            return View(authorVM);
        }

        [Authorize(Roles = "admin")]
        public ActionResult EditAuthor(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            GetAuthorLiteratureView authorVM = service.GetAuthor(id);
            if (authorVM == null)
            {
                return HttpNotFound();
            }

            return View(authorVM);
        }

        [Authorize(Roles = "admin")]
        public ActionResult DeleteAuthor(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            GetAuthorLiteratureView author = service.GetAuthor(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAuthor(GetAuthorLiteratureView author)
        {
            try
            {
                service.DeleteAuthor(author.AuthorID);
            }
            catch (DataException)
            {
                return RedirectToAction("DeleteAuthor", new { id = author.AuthorID });
            }
            return RedirectToAction("AuthorView", "Author");
        }
    }
}