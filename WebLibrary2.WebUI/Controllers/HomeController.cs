using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebLibrary2.Domain.Abstract;
using WebLibrary2.Domain.Concrete;
using WebLibrary2.Domain.Entity;
using System.Data.Entity;
using System.Net;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;

namespace WebLibrary2.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private EFDbContext context;
        IAuthorsRepository repositry;

        public HomeController(IAuthorsRepository authorRepo, EFDbContext dataContext)
        {
            this.repositry = authorRepo;
            this.context = dataContext;
        }

        public ActionResult Index()
        {
            var author = context.Authors.ToList();

            //JSON Serialization
            using (StreamWriter fs = new StreamWriter(@"C:\Users\Anuitex-53\Documents\Visual Studio 2017\WebLibrary2-master\WebLibrary2\JsonAuthors.json"))
            {
                JsonSerializer jsonSerializer = new JsonSerializer();
                jsonSerializer.Serialize(fs,author);
            }

            //XML Serialization
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<Author>));

            using (FileStream fs = new FileStream(@"C:\Users\Anuitex-53\Documents\Visual Studio 2017\WebLibrary2-master\WebLibrary2\XmlAuthors.xml",FileMode.OpenOrCreate))
            {
                XmlSerializer.Serialize(fs,author);
            }

            return View(author);
        }   

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            var author = context.Authors.ToList();
            //using (StreamReader sr = new StreamReader(@"C:\Users\Anuitex-53\Documents\Visual Studio 2017\WebLibrary2-master\WebLibrary2\Authors.json"))
            //{
            //    JsonSerializer serializer = new JsonSerializer();
            //    //Author authors = (Author)serializer.Deserialize(sr,typeof(Author));
            //    JsonConvert.DeserializeObject<List<Author>>(sr.ReadToEnd());
            //}
            XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<Author>));

            using (FileStream fs = new FileStream(@"C:\Users\Anuitex-53\Documents\Visual Studio 2017\WebLibrary2-master\WebLibrary2\XmlAuthors.xml", FileMode.OpenOrCreate))
            {
                List<Author> authors = (List<Author>)XmlSerializer.Deserialize(fs);
                ViewBag.Serialisation = author.ToList();
            }

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose();
            base.Dispose(disposing);
        }

    }
}