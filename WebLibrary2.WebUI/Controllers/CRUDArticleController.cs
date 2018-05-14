using System.Net;
using System.Web.Mvc;
using WebLibrary2.BusinessLogicLayer.Sevices;
using WebLibrary2.ViewModelsLayer.ViewModels;

namespace WebLibrary2.WebUI.Controllers
{
    public class CRUDArticleController : Controller
    {
        private readonly ArticleService articleService;
        private readonly AuthorService authorService;

        public CRUDArticleController(ArticleService articleService, AuthorService authorService)
        { 
            this.articleService = articleService;
            this.authorService = authorService;
        }

        [HttpGet]
        public ActionResult CreateArticle()
        {
            SelectList genres = new SelectList(articleService.GetAllArticles(), "ArticleGenreID", "ArticleGenreName");
            MultiSelectList authors = new MultiSelectList(authorService.GetAllAuthors(), "AuthorID", "AuthorName");
            ViewData["ArticleGenres"] = genres;
            ViewData["Authors"] = authors;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateArticle(GetArticleView articleVM)
        {
            if (ModelState.IsValid)
            {
                articleService.CreateArticle(articleVM);
                return RedirectToAction("Index", "Home");
            }
            SelectList genres = new SelectList(articleService.GetAllArticles(), "ArticleGenreID", "ArticleGenreName");
            MultiSelectList authors = new MultiSelectList(authorService.GetAllAuthors(), "AuthorID", "AuthorName");
            ViewData["ArticleGenres"] = genres;
            ViewData["Authors"] = authors;
            return View(articleVM);
        }


        [Authorize(Roles = "user,admin")]
        [HttpGet]
        public ActionResult ArticleDetails(int id)
        {
            var articleVM = articleService.GetArticleDetails(id);
            return View(articleVM);
        }

        [HttpGet]
        public ActionResult EditArticle(int id)
        {
            var article = articleService.GetArticleDetails(id);
            SelectList genres = new SelectList(articleService.GetAllArticles(), "ArticleGenreID", "ArticleGenreName", article.ArticleGenreID);
            ViewData["ArticleGenres"] = genres;

            MultiSelectList authors = new MultiSelectList(articleService.GetAuthorsNotExistInArticle(article), "AuthorID", "AuthorName", article.Authors);
            ViewData["Authors"] = authors;

            return View("EditArticle", article);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditArticle(GetAllArticlesView articleVM, int[] authorIDsForDelete, int[] authorIDsForInsert)
        {
            if (articleVM == null)
            {
                HttpNotFound();
            }

            if (TryUpdateModel(articleVM))
            {
                articleService.EditArticle(articleVM, authorIDsForDelete, authorIDsForInsert);

                return RedirectToAction("Index", "Home");
            }
            var article = articleService.GetArticleDetails(articleVM.ArticleID);

            SelectList genres = new SelectList(articleService.GetAllArticles(), "ArticleGenreID", "ArticleGenreName", articleVM.ArticleGenreID);
            ViewData["ArticleGenres"] = genres;

            MultiSelectList authors = new MultiSelectList(articleService.GetAuthorsNotExistInArticle(article), "AuthorID", "AuthorName", article.Authors);
            ViewData["Authors"] = authors;

            return View("EditArticle", article);
        }

        [HttpGet]
        public ActionResult DeleteArticle(int id)
        {

            var articleVM = articleService.GetArticleDetails(id);
            if (articleVM == null)
            {
                HttpNotFound();
            }
            return View(articleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteArticle(int? id)
        {
            articleService.DeleteArticle((int)id);
            return RedirectToAction("Index", "Home");
        }
    }
}