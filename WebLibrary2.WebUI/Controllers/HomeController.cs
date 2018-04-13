using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using WebLibrary2.Domain.Concrete;
using WebLibrary2.Domain.Entity;
using System.IO;
using Newtonsoft.Json;
using System.Xml.Serialization;
using WebLibrary2.Domain.Abstract.AbstractAuthor;
using WebLibrary2.Domain.Models;
using WebLibrary2.Domain.Entity.ArticleEntity;
using WebLibrary2.Domain.Abstract.AbstractArticle;
using System;
using System.Web;

namespace WebLibrary2.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private EFDbContext context;
        IArticleRepository articlesRepository;

        private string pathXML;
        private string pathJSON;
        private string serializeFolderPath;

        public HomeController(IArticleRepository articleRepository, EFDbContext dataContext)
        {
            this.context = dataContext;
            articleRepository = articlesRepository;
            pathXML = @"C:\Users\Anuitex-64\source\repos\WebLibrary2\Serialization\XmlAuthors.xml";
            pathJSON = @"C:\Users\Anuitex-64\source\repos\WebLibrary2\Serialization\JsonAuthors.json";

            var userProfilePath = Environment.GetEnvironmentVariable("USERPROFILE");
            serializeFolderPath = Path.Combine(userProfilePath, @"source\repos\WebLibrary2\Serialization");

        }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult SerializeJSON(int[] articleSerializationID)
        {
            List<Article> articlesToSerialize = new List<Article>();

            foreach (int article in articleSerializationID.ToList())
            {
                Article articleToSerialize = context.Articles.Find(article);

                articlesToSerialize.Add(articleToSerialize);
            }
            using (StreamWriter streamWriter = new StreamWriter(new FileStream(serializeFolderPath + @"\JsonArticles.json", FileMode.OpenOrCreate)))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(streamWriter, articlesToSerialize);
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult DeserializeJSON()
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(new FileStream(serializeFolderPath+ @"\JsonArticles.json",FileMode.Open)))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    ViewData["ArticleDataJSON"] = JsonConvert.DeserializeObject<List<Article>>(streamReader.ReadToEnd());
                }
            }
            catch (FileNotFoundException)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Deserialize");
        }


        public ActionResult SerializeXML()
        {
            var author = context.Authors.ToList();
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<Author>));

            using (FileStream fs = new FileStream(pathXML, FileMode.OpenOrCreate))
            {
                XmlSerializer.Serialize(fs, author);
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult DeserializeXML()
        {
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<Author>));
            try
            {
                using (FileStream fs = new FileStream(pathXML, FileMode.Open))
                {
                    List<Author> authors = (List<Author>)XmlSerializer.Deserialize(fs);
                    ViewData["AuthorDataXML"] = authors;
                }
            }
            catch (FileNotFoundException)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Deserialize");
        }

        public ActionResult Deserialize(HttpPostedFile file)
        {
            if (file != null && file.ContentLength > 0 )
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(serializeFolderPath, fileName);
            }

            return View("Deserialize");
        }


        protected override void Dispose(bool disposing)
        {
            context.Dispose();
            base.Dispose(disposing);
        }
    }
}