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
        IAuthorsRepository repository;

        public CRUDAuthorController(IAuthorsRepository authorRepository, EFDbContext dataContext)
        {
            this.context = dataContext;
            this.repository = authorRepository;
        }

        public ActionResult CreateAuthor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAuthor(Author author)
        {
            if (ModelState.IsValid)
            {
                CreateAuthor(author);
                return RedirectToAction("Index", "Home");
            }
            return View(author);
        }

        public ActionResult AuthorsDetails(int id = 0)
        {
            var authorVM = repository.GetBookDetails(id);
            return View(authorVM);
        }

        public ActionResult EditAuthor(int? id)
        {
            Author author = repository.GetAuthorByID(id);
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
            var authorToUpdate = repository.GetAuthorByID(id);

            if (TryUpdateModel(authorToUpdate))
            {
                try
                {
                    repository.Save();
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
            Author author = repository.GetAuthorByID(id);
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
                Author author = repository.GetAuthorByID(id);
                repository.DeleteAuthor(author);
            }
            catch (DataException)
            {
                return RedirectToAction("DeleteAuthor", new { id = id });
            }
            return RedirectToAction("Index", "Home");
        }

    }
}