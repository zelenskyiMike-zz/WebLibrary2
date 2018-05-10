using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using WebLibrary2.BusinessLogicLayer.Sevices;
using WebLibrary2.Domain.Extensions;
using WebLibrary2.ViewModelsLayer.ViewModels;

namespace WebLibrary2.WebUI.Controllers
{
    public class ArticlesController : Controller
    {
        string serializeFolderPath;
        private string filePath;

        private Regex regexJSON;
        private Regex regexXML;

        private MatchCollection matchXML;
        private MatchCollection matchJSON;
        private readonly ArticleService articleService;

        public ArticlesController(ArticleService articleService)
        {
            var userProfilePath = Environment.GetEnvironmentVariable("USERPROFILE");
            serializeFolderPath = Path.Combine(userProfilePath, @"source\repos\WebLibrary2\Serialization");
            this.articleService = articleService;
        }
        // GET: Articles
        [HttpGet]
        public PartialViewResult ArticlesView()
        {
            var articles = articleService.GetAllArticlesWithGenres();
            return PartialView(articles);
        }

        [HttpPost]
        public ActionResult SerializeArticleToJSON(int[] articleSerializationID, string fileName)
        {
            filePath = serializeFolderPath + "\\" + fileName + ".json";
            if (articleSerializationID != null)
            {
                List<GetArticleView> articlesToSerialize = new List<GetArticleView>();
                List<GetArticleView> articlesFromFile = DeserializationExtensionClass.DeserializeJSON<GetArticleView>(filePath);
                if (articlesFromFile != null)
                {
                    articlesToSerialize = articlesFromFile;
                }
                foreach (int article in articleSerializationID.ToList())
                {
                    GetArticleView articleToSerialize = articleService.GetArticleByID(article);
                    if (!articlesToSerialize.Contains(articleToSerialize))
                    {
                        articlesToSerialize.Add(articleToSerialize);
                    }
                }

                FileStream fileStream = new FileStream(filePath, FileMode.OpenOrCreate);
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    JsonSerializer jsonSerializer = new JsonSerializer();
                    jsonSerializer.Serialize(streamWriter, articlesToSerialize);
                }
                return RedirectToAction("Index", "Home");
            }
            Exception nullEx = new Exception("There is nothing to serialize");
            return View("Error", new HandleErrorInfo(nullEx, "Articles", "ArticlesView"));
        }

        [HttpPost]
        public ActionResult SerializeArticleToXML(int[] articleSerializationID, string fileName)
        {
            filePath = serializeFolderPath + "\\"+ fileName + ".xml";
            if (articleSerializationID != null)
            {
                List<GetArticleView> articlesToSerialize = new List<GetArticleView>();

                List<GetArticleView> articlesFromFile = DeserializationExtensionClass.DeserializeXML<GetArticleView>(filePath);
                if (articlesFromFile != null)
                {
                    articlesToSerialize = articlesFromFile;
                }
                foreach (var article in articleSerializationID.ToList())
                {
                    GetArticleView articleToSerialize = articleService.GetArticleByID(article);
                    if (!articlesToSerialize.Contains(articleToSerialize))
                    {
                        articlesToSerialize.Add(articleToSerialize);
                    }
                }

                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<GetArticleView>));
                    XmlSerializer.Serialize(fs, articlesToSerialize);
                }
                return RedirectToAction("Index", "Home");
            }
            Exception nullEx = new Exception("There is nothing to serialize");
            return View("Error", new HandleErrorInfo(nullEx, "Articles", "ArticlesView"));
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
                        List<GetArticleView> fileContent = DeserializationExtensionClass.DeserializeJSON<GetArticleView>(filePath);
                        for (int i = 0; i < 1; i++)
                        {
                            if (fileContent[i].ArticleID == 0)
                            {
                                throw new Exception("Wrong filef for this publications type. Please, choose another file");
                            }
                        }
                        ViewData["ArticleDataJSON"] = fileContent;
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
                        List<GetArticleView> fileContent = DeserializationExtensionClass.DeserializeXML<GetArticleView>(filePath);
                        for (int i = 0; i < 1; i++)
                        {
                            if (fileContent == null)
                            {
                                throw new Exception("Wrong filef for this publications type. Please, choose another file");
                            }
                        }
                        ViewData["ArticleDataXML"] = fileContent;
                    }
                    catch (Exception ex)
                    {
                        return View("Error", new HandleErrorInfo(ex, "Articles", "ArticlesView"));
                    }
                    return View();
                }
            }
            Exception nullEx = new Exception("File is null");
            return View("Error", new HandleErrorInfo(nullEx, "Articles", "ArticlesView"));
        }
    }
}