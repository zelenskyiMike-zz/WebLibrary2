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

namespace WebLibrary2.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private EFDbContext context;
        IAuthorsRepository repositry;

        private string pathXML;
        private string pathJSON;

        public HomeController(IAuthorsRepository authorRepo, EFDbContext dataContext)
        {
            this.repositry = authorRepo;
            this.context = dataContext;
            pathXML = @"C:\Users\Anuitex-64\source\repos\WebLibrary2\Serialization\XmlAuthors.xml";
            pathJSON = @"C:\Users\Anuitex-64\source\repos\WebLibrary2\Serialization\JsonAuthors.json";
       }
        [HttpGet]
        public ActionResult Index()
        {
            /* mb Repo? */
            //var books = context.Books.Include(bg => bg.Genres).ToList();
            //var articles = context.Articles.Include(ag => ag.ArticleGenres).ToList();
            //var magazines = context.Magazines.Include(mg => mg.MagazineGenres).ToList();
            //var publications = context.Publications.Include(pg => pg.PublicationGenres).ToList();

            //GetLiteratureViewModel literature = new GetLiteratureViewModel()
            //{
            //    Books = books,
            //    Articles = articles,
            //    Magazines = magazines,
            //    Publications = publications
            //};
            return View(/*literature*/);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult SerializeJSON()
        {
            var author = context.Authors.ToList();
            using (StreamWriter fs = new StreamWriter(pathJSON))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(fs, author);
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult DeserializeJSON()
        {
            try
            {
                using (StreamReader sr = new StreamReader(pathJSON))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    ViewData["AuthorDataJSON"] = JsonConvert.DeserializeObject<List<Author>>(sr.ReadToEnd());
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

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
            base.Dispose(disposing);
        }

    }
}