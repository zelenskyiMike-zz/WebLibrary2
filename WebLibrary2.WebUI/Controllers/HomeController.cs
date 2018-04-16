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
using WebLibrary2.Domain.Entity.BookEntity;
using WebLibrary2.Domain.Entity.MagazineEntity;
using WebLibrary2.Domain.Entity.PublicationEntity;

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
        public ActionResult SerializeArticleToJSON(int[] articleSerializationID)
        {
            if (articleSerializationID != null)
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
            //--------------------------------------------------------------------------------//
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult SerializeBookToJSON(int[] bookSerializationID)
        {
            if (bookSerializationID != null)
            { 
                List<Book> booksToSerialize = new List<Book>();

                foreach (int book in bookSerializationID.ToList())
                {
                    Book bookToSerialize = context.Books.Find(book);

                    booksToSerialize.Add(bookToSerialize);
                }
                using (StreamWriter streamWriter = new StreamWriter(new FileStream(serializeFolderPath + @"\JsonBooks.json", FileMode.OpenOrCreate)))
                {
                    JsonSerializer jsonSerializer = new JsonSerializer();
                    jsonSerializer.Serialize(streamWriter, booksToSerialize);
                }
                return RedirectToAction("Index", "Home");
            }
            //--------------------------------------------------------------------------------//
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult SerializeMagazineToJSON(int[] magazineSerializationID)
        {
            if (magazineSerializationID != null)
            {
                List<Magazine> magazinesToSerialize = new List<Magazine>();

                foreach (int magazine in magazineSerializationID.ToList())
                {
                    Magazine magazineToSerialize = context.Magazines.Find(magazine);

                    magazinesToSerialize.Add(magazineToSerialize);
                }
                using (StreamWriter streamWriter = new StreamWriter(new FileStream(serializeFolderPath + @"\JsonMagazines.json", FileMode.OpenOrCreate)))
                {
                    JsonSerializer jsonSerializer = new JsonSerializer();
                    jsonSerializer.Serialize(streamWriter, magazinesToSerialize);
                }
                return RedirectToAction("Index", "Home");
            }
            //--------------------------------------------------------------------------------//
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public ActionResult SerializePublicationToJSON(int[] publicationSerializationID)
        {
            if (publicationSerializationID != null)
            {
                List<Publication> publicationsToSerialize = new List<Publication>();

                foreach (int publication in publicationSerializationID.ToList())
                {
                    Publication publicationToSerialize = context.Publications.Find(publication);

                    publicationsToSerialize.Add(publicationToSerialize);
                }
                using (StreamWriter streamWriter = new StreamWriter(new FileStream(serializeFolderPath + @"\JsonPublications.json", FileMode.OpenOrCreate)))
                {
                    JsonSerializer jsonSerializer = new JsonSerializer();
                    jsonSerializer.Serialize(streamWriter, publicationsToSerialize);
                }
                return RedirectToAction("Index", "Home");
            }
            //--------------------------------------------------------------------------------//
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public ActionResult SerializeBookToXML(int[] bookSerializationID)
        {
            if (bookSerializationID != null)
            {
                List<Book> booksToSerialize = new List<Book>();

                foreach (var book in bookSerializationID.ToList())
                {
                    Book bookToSerialize = context.Books.Find(book);

                    booksToSerialize.Add(bookToSerialize);
                }

                using (FileStream fs = new FileStream(serializeFolderPath + @"\BooksXML.xml", FileMode.OpenOrCreate))
                {
                    XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<Book>));
                    XmlSerializer.Serialize(fs, booksToSerialize);
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public ActionResult SerializeArticleToXML(int[] articleSerializationID)
        {
            if (articleSerializationID != null)
            {
                List<Article> articlesToSerialize = new List<Article>();

                foreach (var article in articleSerializationID.ToList())
                {
                    Article articleToSerialize = context.Articles.Find(article);

                    articlesToSerialize.Add(articleToSerialize);
                }

                using (FileStream fs = new FileStream(serializeFolderPath + @"\ArticlesXML.xml", FileMode.OpenOrCreate))
                {
                    XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<Article>));
                    XmlSerializer.Serialize(fs, articlesToSerialize);
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public ActionResult SerializeMagazineToXML(int[] magazineSerializationID)
        {
            if (magazineSerializationID != null)
            {
                List<Magazine> magazinesToSerialize = new List<Magazine>();

                foreach (var article in magazineSerializationID.ToList())
                {
                    Magazine magazineToSerialize = context.Magazines.Find(article);

                    magazinesToSerialize.Add(magazineToSerialize);
                }

                using (FileStream fs = new FileStream(serializeFolderPath + @"\MagazinesXML.xml", FileMode.OpenOrCreate))
                {
                    XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<Magazine>));
                    XmlSerializer.Serialize(fs, magazinesToSerialize);
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");

        }
        [HttpPost]
        public ActionResult SerializePublicationToXML(int[] publicationSerializationID)
        {
            if (publicationSerializationID != null)
            {
                List<Publication> publicationsToSerialize = new List<Publication>();

                foreach (var article in publicationSerializationID.ToList())
                {
                    Publication publicationToSerialize = context.Publications.Find(article);

                    publicationsToSerialize.Add(publicationToSerialize);
                }

                using (FileStream fs = new FileStream(serializeFolderPath + @"\PublicationsXML.xml", FileMode.OpenOrCreate))
                {
                    XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<Publication>));
                    XmlSerializer.Serialize(fs, publicationsToSerialize);
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");

        }


        public ActionResult DeserializeJSON()
        {
            try
            {
                using (StreamReader streamReader = new StreamReader(new FileStream(serializeFolderPath + @"\JsonArticles.json", FileMode.Open)))
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
        public ActionResult DeserializeXML(HttpPostedFileBase file)
        {
            string filePath = GetFilePath(file);

            XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<Article>));
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    List<Article> authors = (List<Article>)XmlSerializer.Deserialize(fs);
                    ViewData["ArticleDataXML"] = authors;
                }
            }
            catch (FileNotFoundException)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Deserialize");
        }

        public string GetFilePath(HttpPostedFileBase file)
        {
            string path;

            if (file != null && file.ContentLength > 0 )
            {
                var fileName = Path.GetFileName(file.FileName);
                path = Path.Combine(serializeFolderPath, fileName);
                return path;
            }
            throw new NotImplementedException();
        }


        protected override void Dispose(bool disposing)
        {
            context.Dispose();
            base.Dispose(disposing);
        }
    }
}