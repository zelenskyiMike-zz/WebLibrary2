using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebLibrary2.Domain.Abstract.AbstractBook;
using WebLibrary2.Domain.Entity.BookEntity;
using WebLibrary2.Domain.Extensions;

namespace WebLibrary2.WebUI.Controllers.BookControllers
{
    public class BooksController : Controller
    {
        string serializeFolderPath;
        private string filePath;

        private MatchCollection matchXML;
        private MatchCollection matchJSON;
        private Regex regexJSON;
        private Regex regexXML;

        IBookRepository booksRepository;

        public BooksController(IBookRepository bookRepository)
        {
            booksRepository = bookRepository;

            var userProfilePath = Environment.GetEnvironmentVariable("USERPROFILE");
            serializeFolderPath = Path.Combine(userProfilePath, @"source\repos\WebLibrary2\Serialization");
        }

        public PartialViewResult BooksView()
        {
            var book = booksRepository.GetAllBooksWithGenres();
                   
            return PartialView(book);
        }
        [HttpPost]
        public ActionResult DeserializeBook(HttpPostedFileBase file)
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
                        ViewData["BookDataJSON"] = DeserializationExtensionClass.DeserializeJSON<Book>(filePath);
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
                        ViewData["BookDataXML"] = DeserializationExtensionClass.DeserializeXML<Book>(filePath);
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
