using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebLibrary2.Domain.Abstract.AbstractArticle;
using WebLibrary2.Domain.Concrete;
using WebLibrary2.Domain.Models;

namespace WebLibrary2.WebUI.Controllers.ArticleControllers
{
    public class CRUDArticleController : Controller
    {
        EFDbContext context;
        IArticleRepository articleRepository;
        IArticeAuthorsRepository articleAuthorsRepository;

        public CRUDArticleController(EFDbContext contextParam, IArticleRepository articlesRepository, IArticeAuthorsRepository articlesAuthorsRepository)
        {
            context = contextParam;
            articleRepository = articlesRepository;
            articleAuthorsRepository = articlesAuthorsRepository;
        }

        [HttpGet]
        public ActionResult CreateArticle()
        {
            SelectList genres = new SelectList(context.ArticleGenres, "ArticleGenreID", "ArticleGenreName");
            MultiSelectList authors = new MultiSelectList(context.Authors, "AuthorID", "AuthorName");
            ViewData["ArticleGenres"] = genres;
            ViewData["Authors"] = authors;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateArticle(ArticleViewModel articleVM)
        {
            if (ModelState.IsValid)
            {
                articleRepository.InsertArticle(articleVM);
                return RedirectToAction("Index", "Home");
            }
            SelectList genres = new SelectList(context.ArticleGenres, "ArticleGenreID", "ArticleGenreName");
            MultiSelectList authors = new MultiSelectList(context.Authors, "AuthorID", "AuthorName");
            ViewData["ArticleGenres"] = genres;
            ViewData["Authors"] = authors;
            return View(articleVM);
        }

        [HttpGet]
        public ActionResult ArticleDetails(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var articleVM = articleRepository.GetArticleDetails(id);
            return View(articleVM);
        }

        [HttpGet]
        public ActionResult EditArticle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var article = articleRepository.GetArticleDetails(id);

            if (article == null)
            {
                HttpNotFound();
            }

            SelectList genres = new SelectList(context.ArticleGenres, "ArticleGenreID", "ArticleGenreName", article.ArticleGenreID);
            ViewData["ArticleGenres"] = genres;

            MultiSelectList authors = new MultiSelectList(articleRepository.GetAuthorsNotExistInArticle((int)id), "AuthorID", "AuthorName", article.Authors);
            ViewData["Authors"] = authors;

            return View(article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditArticle(GetM2MCRUDArticleVM articleVM, int[] authorIDsForDelete, int[] authorIDsForInsert)
        {
            if (articleVM == null)
            {
                HttpNotFound();
            }

            var articleToUpdate = articleRepository.GetArticleByID(articleVM.ArticleID);

            if (TryUpdateModel(articleToUpdate))
            {
                try
                {
                    articleAuthorsRepository.DeleteAuthorFromArticle(articleToUpdate.ArticleID, authorIDsForDelete);
                    articleAuthorsRepository.AddAuthorToArticle(articleToUpdate.ArticleID, authorIDsForInsert);
                    articleRepository.Save();
                }
                catch (DataException dex)
                {
                    ModelState.AddModelError("", "Unable to save");
                    Console.WriteLine(dex);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult DeleteArticle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GetM2MCRUDArticleVM articleVM = articleRepository.GetArticleDetails(id);
            if (articleVM == null)
            {
                HttpNotFound();
            }
            return View(articleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteArticle(int id)
        {
            articleRepository.DeleteArticle(id);
            return RedirectToAction("Index", "Home");
        }
    }
}