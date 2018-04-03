using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebLibrary2.Domain.Abstract.AbstractArticle;

namespace WebLibrary2.WebUI.Controllers.ArticleControllers
{
    public class ArticlesController : Controller
    {
        IArticleRepository articlesRepository;
        public ArticlesController(IArticleRepository articleRepository)
        {
            articlesRepository = articleRepository;
        }
        // GET: Articles
        [HttpPost]
        public ActionResult ArticlesView()
        {
            var articles = articlesRepository.GetAllArticlesWithGenres();
            return PartialView(articles);
        }
    }
}