using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebLibrary2.Domain.Abstract;
using WebLibrary2.Domain.Concrete;
using WebLibrary2.Domain.Entity;
using System.Data.Entity;
using System.Net;
using System.Data;
using WebLibrary2.Domain.Models;

namespace WebLibrary2.WebUI.Controllers
{
    public class CRUDAuthorController : Controller
    {
        private EFDbContext context;
        IAuthorsRepository authorRepository;

        public CRUDAuthorController(IAuthorsRepository authorsRepository, EFDbContext dataContext)
        {
            this.context = dataContext;
            this.authorRepository = authorsRepository;
        }

        public ActionResult CreateAuthor()
        {
            MultiSelectList books = new MultiSelectList(context.Books, "BookID", "BookName");
            ViewData["Books"] = books;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAuthor(AuthorViewModel authorVM)
        {
            if (ModelState.IsValid)
            {
                authorRepository.CreateAuthor(authorVM);
                return RedirectToAction("Index", "Home");
            }
            return View(authorVM);
        }

        public ActionResult AuthorsDetails(int id = 0)
        {
            var authorVM = authorRepository.GetAuthorDetails(id);
            return View(authorVM);
        }

        public ActionResult EditAuthor(int? id)
        {
            Author author = authorRepository.GetAuthorByID(id);
            if (author == null)
            {
                return HttpNotFound();
            }

            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAuthor(int id)
        {
            var authorToUpdate = authorRepository.GetAuthorByID(id);

            if (TryUpdateModel(authorToUpdate))
            {
                try
                {
                    authorRepository.Save();
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save");
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteAuthor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var author = authorRepository.GetAuthorDetails(id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAuthor(int id)
        {
            try
            {
                authorRepository.DeleteAuthor(id);
            }
            catch (DataException)
            {
                return RedirectToAction("DeleteAuthor", new { id = id });
            }
            return RedirectToAction("Index", "Home");
        }
    }
}