using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebLibrary2.Domain.Abstract.AbstractBook;
using WebLibrary2.Domain.Concrete.ConcreteBook;
using WebLibrary2.Domain.Entity.BookEntity;
using WebLibrary2.Domain.Extensions;

namespace WebLibrary2.WebUI.Controllers.BookControllers
{
    public class BooksController : Controller
    {
        string serializeFolderPath;
        private string filePath;

        private Regex regexJSON;
        private Regex regexXML;
        private Regex regexValidation;

        private MatchCollection matchXML;
        private MatchCollection matchJSON;
        private MatchCollection matchValidation;

        EFBookRepository booksRepository;

        public BooksController(EFBookRepository bookRepository)
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
            regexValidation = new Regex(@"(\w*)Book(\w*)");

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
                        ViewData["BookDataJSON"] = DeserializationExtensionClass.DeserializeJSON<Book>(filePath);
                    }
                    catch (Exception ex)
                    {
                        return View("Error", new HandleErrorInfo(ex,"Books","BooksView"));
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
                        ViewData["BookDataXML"] = DeserializationExtensionClass.DeserializeXML<Book>(filePath);
                    }
                    catch (Exception ex)
                    {
                        return View("Error", new HandleErrorInfo(ex, "Books", "BooksView"));
                    }
                    return View();
                }
            }
            throw new Exception("File is null");
        }
    }
}
