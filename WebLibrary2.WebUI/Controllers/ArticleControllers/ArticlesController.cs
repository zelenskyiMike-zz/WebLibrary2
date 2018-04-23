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

        private Regex regexJSON;
        private Regex regexXML;
        private Regex regexValidation;

        private MatchCollection matchXML;
        private MatchCollection matchJSON;
        private MatchCollection matchValidation;

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
            regexValidation = new Regex(@"(\w*)Article(\w*)");

            if (file != null)
            {
                filePath = FilePath.GetFilePath(file, serializeFolderPath);

                matchJSON = regexJSON.Matches(filePath);
                matchXML = regexXML.Matches(filePath);
                matchValidation = regexValidation.Matches(filePath);


                if (matchJSON.Count != 0)
                {
                    try
                    {
                        if (matchValidation.Count == 0)
                        {
                            throw new Exception("Wrong filef for this publications type. Please, choose another file");
                        }
                        ViewData["ArticleDataJSON"] = DeserializationExtensionClass.DeserializeJSON<Article>(filePath);
                    }
                    catch (Exception ex)
                    {
                        return View("Error", new HandleErrorInfo(ex, "Articles", "ArticlesView"));
                    }
                    return View();
                }
                if (matchXML.Count != 0)
                {
                    try
                    {
                        if (matchValidation.Count == 0)
                        {
                            throw new Exception("Wrong filef for this publications type. Please, choose another file");
                        }
                        ViewData["ArticleDataXML"] = DeserializationExtensionClass.DeserializeXML<Article>(filePath);
                    }
                    catch (Exception ex)
                    {
                        return View("Error", new HandleErrorInfo(ex, "Articles", "ArticlesView"));
                    }
                    return View();
                }
            }
            throw new Exception("File is null");
        }
    }
}