using System;
using System.Web;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using WebLibrary2.Domain.Concrete;
using WebLibrary2.Domain.Entity.ArticleEntity;
using WebLibrary2.Domain.Entity.BookEntity;
using WebLibrary2.Domain.Entity.MagazineEntity;
using WebLibrary2.Domain.Entity.PublicationEntity;
using WebLibrary2.Domain.Extensions;



namespace WebLibrary2.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private EFDbContext context;
        //IArticleRepository articlesRepository;

        private string serializeFolderPath;
        private string filePath;

        public HomeController(EFDbContext dataContext)
        {
            this.context = dataContext;
            
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
            filePath = serializeFolderPath + @"\JsonArticles.json";
            if (articleSerializationID != null)
            {
                List<Article> articlesToSerialize = new List<Article>();
                articlesToSerialize = DeserializationExtensionClass.DeserializeJSON<Article>(filePath); 

                foreach (int article in articleSerializationID.ToList())
                {
                    Article articleToSerialize = context.Articles.Find(article);
                    if (/*doesnt work*/!articlesToSerialize.Contains(articleToSerialize))
                    {
                        articlesToSerialize.Add(articleToSerialize);
                    } 
                }

                using (StreamWriter streamWriter = new StreamWriter(new FileStream(filePath, FileMode.OpenOrCreate)))
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
            filePath = serializeFolderPath + @"\JsonBooks.json";
            if (bookSerializationID != null)
            { 
                List<Book> booksToSerialize = new List<Book>();
                booksToSerialize = DeserializationExtensionClass.DeserializeJSON<Book>(filePath);

                foreach (int book in bookSerializationID.ToList())
                {
                    Book bookToSerialize = context.Books.Find(book);

                    if (!booksToSerialize.Contains(bookToSerialize))
                    {
                        booksToSerialize.Add(bookToSerialize);
                    }
                }
                using (StreamWriter streamWriter = new StreamWriter(new FileStream(filePath, FileMode.OpenOrCreate)))
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
            filePath = serializeFolderPath + @"\JsonMagazines.json";
            if (magazineSerializationID != null)
            {
                List<Magazine> magazinesToSerialize = new List<Magazine>();
                magazinesToSerialize = DeserializationExtensionClass.DeserializeJSON<Magazine>(filePath);

                foreach (int magazine in magazineSerializationID.ToList())
                {
                    Magazine magazineToSerialize = context.Magazines.Find(magazine);
                    if (!magazinesToSerialize.Contains(magazineToSerialize))
                    {
                        magazinesToSerialize.Add(magazineToSerialize);
                    }
                }
                using (StreamWriter streamWriter = new StreamWriter(new FileStream(filePath, FileMode.OpenOrCreate)))
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
            filePath = serializeFolderPath + @"\JsonPublications.json";
            if (publicationSerializationID != null)
            {
                List<Publication> publicationsToSerialize = new List<Publication>();
                publicationsToSerialize = DeserializationExtensionClass.DeserializeJSON<Publication>(filePath); ;
                foreach (int publication in publicationSerializationID.ToList())
                {
                    Publication publicationToSerialize = context.Publications.Find(publication);
                    if (!publicationsToSerialize.Contains(publicationToSerialize))
                    {
                        publicationsToSerialize.Add(publicationToSerialize);
                    }
                }
                using (StreamWriter streamWriter = new StreamWriter(new FileStream(filePath, FileMode.OpenOrCreate)))
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
            filePath = serializeFolderPath + @"\BooksXML.xml";
            if (bookSerializationID != null)
            {
                List<Book> booksToSerialize = new List<Book>();
                booksToSerialize = DeserializationExtensionClass.DeserializeXML<Book>(filePath);
                foreach (var book in bookSerializationID.ToList())
                {
                    Book bookToSerialize = context.Books.Find(book);
                    if (!booksToSerialize.Contains(bookToSerialize))
                    {
                        booksToSerialize.Add(bookToSerialize);
                    }
                }

                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
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
            filePath = serializeFolderPath + @"\ArticlesXML.xml";
            if (articleSerializationID != null)
            {
                List<Article> articlesToSerialize = new List<Article>();
                articlesToSerialize = DeserializationExtensionClass.DeserializeJSON<Article>(filePath);
                foreach (var article in articleSerializationID.ToList())
                {
                    Article articleToSerialize = context.Articles.Find(article);
                    if (!articlesToSerialize.Contains(articleToSerialize))
                    {
                        articlesToSerialize.Add(articleToSerialize);
                    }
                }

                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
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
            filePath = serializeFolderPath + @"\MagazinesXML.xml";
            if (magazineSerializationID != null)
            {
                List<Magazine> magazinesToSerialize = new List<Magazine>();
                magazinesToSerialize = DeserializationExtensionClass.DeserializeJSON<Magazine>(filePath);

                foreach (var article in magazineSerializationID.ToList())
                {
                    Magazine magazineToSerialize = context.Magazines.Find(article);
                    if (!magazinesToSerialize.Contains(magazineToSerialize))
                    {
                        magazinesToSerialize.Add(magazineToSerialize);
                    }
                }

                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
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
            filePath = serializeFolderPath + @"\PublicationsXML.xml";
            if (publicationSerializationID != null)
            {
                List<Publication> publicationsToSerialize = new List<Publication>();
                publicationsToSerialize = DeserializationExtensionClass.DeserializeJSON<Publication>(filePath);

                foreach (var article in publicationSerializationID.ToList())
                {
                    Publication publicationToSerialize = context.Publications.Find(article);
                    if (!publicationsToSerialize.Contains(publicationToSerialize))
                    {
                        publicationsToSerialize.Add(publicationToSerialize);
                    }
                }

                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<Publication>));
                    XmlSerializer.Serialize(fs, publicationsToSerialize);
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");

        }

        [HttpPost]
        public ActionResult Deserialize(HttpPostedFileBase file)
        {
            //string filePath;
            Regex regexJSON = new Regex(@"(\w*).json");
            Regex regexXML = new Regex(@"(\w*).xml");


            if (file != null)
            {
                filePath = GetFilePath(file);
            }
            MatchCollection matchJSON = regexJSON.Matches(filePath);
            MatchCollection matchXML = regexXML.Matches(filePath);

            if (matchJSON.Count != 0)
            {
                try
                {
                    ViewData["ArticleDataJSON"]     = DeserializationExtensionClass.DeserializeJSON<Article>(filePath);
                    ViewData["MagazineDataJSON"]    = DeserializationExtensionClass.DeserializeJSON<Magazine>(filePath);
                    ViewData["BookDataJSON"]        = DeserializationExtensionClass.DeserializeJSON<Book>(filePath);
                    ViewData["PublicationDataJSON"] = DeserializationExtensionClass.DeserializeJSON<Publication>(filePath);

                }
                catch (FileNotFoundException)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View("Deserialize");
            }
            if (matchXML.Count != 0)
            {
                try
                {
                    ViewData["ArticleDataXML"] = DeserializationExtensionClass.DeserializeXML<Article>(filePath);
                    ViewData["MagazineDataXML"] = DeserializationExtensionClass.DeserializeXML<Magazine>(filePath);
                    ViewData["BookDataXML"] = DeserializationExtensionClass.DeserializeXML<Book>(filePath);
                    ViewData["PublicationDataXML"] = DeserializationExtensionClass.DeserializeXML<Publication>(filePath);
                }
                catch (FileNotFoundException)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View("Deserialize");
            }
            return RedirectToAction("Index", "Home");
        }

        //public ActionResult DeserializeXML(HttpPostedFileBase file)
        //{
        //    string filePath = "";

        //    if (file != null)
        //    {
        //        filePath = GetFilePath(file);
        //    }
            
        //    XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<Article>));
        //    try
        //    {
        //        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        //        {
        //            List<Article> authors = (List<Article>)XmlSerializer.Deserialize(fs);
        //            ViewData["ArticleDataXML"] = authors;
        //        }
        //    }
        //    catch (FileNotFoundException)
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }
        //    return View("Deserialize");
        //}

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