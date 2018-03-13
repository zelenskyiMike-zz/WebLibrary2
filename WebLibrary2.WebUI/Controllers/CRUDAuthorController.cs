﻿using System;
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

        /// <summary>
        /// ПЕРЕДЕЛАЬ ПОД REPOSITORY, ОБЬЯВЛЕНИЕ ПЕРЕМЕННЫХ В КОНСТРУТОРЕ.
        /// </summary>
        private EFDbContext context = new EFDbContext();
     
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
                context.Authors.Add(author);
                context.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(author);
        }

        public ActionResult AuthorsDetails(int id = 0)
        {
            //Author author = context.Authors.Find(id);
            AddABookViewModel author = new AddABookViewModel()
            {
                Authors = context.Authors,
                Books = context.Books
            };
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        public ActionResult EditAuthor(int? id)
        {
            Author author = context.Authors.Find(id);
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
            var authorToUpdate = context.Authors.Find(id);

            if (TryUpdateModel(authorToUpdate))
            {
                try
                {
                    context.SaveChanges();
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save");
                }
            }
            return RedirectToAction("Index","Home");
        }

        public ActionResult DeleteAuthor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = context.Authors.Find(id);
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
                var author = context.Authors.Find(id);
                context.Authors.Remove(author);
                context.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("DeleteAuthor", new { id = id });
            }
            return RedirectToAction("Index","Home");
        }

    }
}