using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebLibrary2.Domain.Abstract.AbstractArticle;
using WebLibrary2.Domain.Entity.ArticleEntity;
using WebLibrary2.Domain.Extensions;

namespace WebLibrary2.WebUI.Controllers.ArticleControllers
{
    public class ArticlesController : Controller
    {
        string serializeFolderPath;
        private string filePath;

        private MatchCollection matchXML;
        private MatchCollection matchJSON;
        private Regex regexJSON;
        private Regex regexXML;

        IArticleRepository articlesRepository;

        public ArticlesController(IArticleRepository articleRepository)
        {
            articlesRepository = articleRepository;

            var userProfilePath = Environment.GetEnvironmentVariable("USERPROFILE");
            serializeFolderPath = Path.Combine(userProfilePath, @"source\repos\WebLibrary2\Serialization");

        }
        // GET: Articles
        [HttpGet]
        public PartialViewResult ArticlesView()
        {
            var articles = articlesRepository.GetAllArticlesWithGenres();
            return PartialView(articles);
        }

        [HttpPost]
        public ActionResult DeserializeArticle(HttpPostedFileBase file)
        {
            regexJSON = new Regex(@"(\w*).json");
            regexXML = new Regex(@"(\w*).xml");


            if (file != null)
            {
                filePath = FilePath.GetFilePath(file, serializeFolderPath);

                matchJSON = regexJSON.Matches(filePath);
                matchXML = regexXML.Matches(filePath);


                if (matchJSON.Count != 0)
                {
                    try
                    {
                        ViewData["BookDataJSON"] = DeserializationExtensionClass.DeserializeJSON<Article>(filePath);
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return View();
                }
                if (matchXML.Count != 0)
                {
                    try
                    {
                        ViewData["BookDataXML"] = DeserializationExtensionClass.DeserializeXML<Article>(filePath);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    return View();
                }
            }
            throw new Exception("File is null");
        }
    }
}