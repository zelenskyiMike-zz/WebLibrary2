using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using WebLibrary2.Domain.Abstract.AbstractBook;
using WebLibrary2.Domain.Concrete;
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

        private MatchCollection matchXML;
        private MatchCollection matchJSON;

        EFBookRepository booksRepository;
        EFDbContext context;

        public BooksController(EFBookRepository bookRepository, EFDbContext context)
        {
            booksRepository = bookRepository;
            this.context = context;

            var userProfilePath = Environment.GetEnvironmentVariable("USERPROFILE");
            serializeFolderPath = Path.Combine(userProfilePath, @"source\repos\WebLibrary2\Serialization");
        }

        public PartialViewResult BooksView()
        {
            var book = booksRepository.GetAllBooksWithGenres();
                   
            return PartialView(book);
        }

        [HttpPost]
        public ActionResult SerializeBookToJSON(int[] bookSerializationID, string fileName)
        {
            filePath = serializeFolderPath + "\\" + fileName + ".json";
            if (bookSerializationID != null)
            {
                List<Book> booksToSerialize = new List<Book>();

                List<Book> booksfromFile = DeserializationExtensionClass.DeserializeJSON<Book>(filePath);

                if (booksfromFile != null)
                {
                    booksToSerialize = booksfromFile;
                }

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
            Exception nullEx = new Exception("There is nothing to serialize");
            return View("Error", new HandleErrorInfo(nullEx, "Books", "BooksView"));
        }

        [HttpPost]
        public ActionResult SerializeBookToXML(int[] bookSerializationID, string fileName)
        {
            filePath = serializeFolderPath + "\\" + fileName + ".xml";

            if (bookSerializationID != null)
            {
                List<Book> booksToSerialize = new List<Book>();

                List<Book> booksFromFile = DeserializationExtensionClass.DeserializeXML<Book>(filePath);
                if (booksFromFile != null)
                {
                    booksToSerialize = booksFromFile;
                }
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
            Exception nullEx = new Exception("There is nothing to serialize");
            return View("Error", new HandleErrorInfo(nullEx, "Books", "BooksView"));
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
                        List<Book> fileContent = DeserializationExtensionClass.DeserializeJSON<Book>(filePath);
                        for (int i = 0; i < 1; i++)
                        {
                            if (fileContent[i].BookID == 0)
                            {
                                throw new Exception("Wrong filef for this publications type. Please, choose another file");

                            }
                        }
                        ViewData["BookDataJSON"] = fileContent;
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
                        List<Book> fileContent = DeserializationExtensionClass.DeserializeJSON<Book>(filePath);

                        for (int i = 0; i < 1; i++)
                        {
                            if (fileContent == null)
                            {
                                throw new Exception("Wrong filef for this publications type. Please, choose another file");
                            }
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
            Exception nullEx = new Exception("File is null");
            return View("Error", new HandleErrorInfo(nullEx, "Books", "BooksView"));
        }
    }
}
