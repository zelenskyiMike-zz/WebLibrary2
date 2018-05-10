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
    public class BooksController : Controller
    {
        string serializeFolderPath;
        private string filePath;

        private Regex regexJSON;
        private Regex regexXML;

        private MatchCollection matchXML;
        private MatchCollection matchJSON;
        private readonly BookService bookService;

        public BooksController(BookService bookService)
        {
            var userProfilePath = Environment.GetEnvironmentVariable("USERPROFILE");
            serializeFolderPath = Path.Combine(userProfilePath, @"source\repos\WebLibrary2\Serialization");
            this.bookService = bookService;
        }

        public PartialViewResult BooksView()
        {
            var book = bookService.GetAllBooksWithGenres();
                   
            return PartialView(book);
        }

        [HttpPost]
        public ActionResult SerializeBookToJSON(int[] bookSerializationID, string fileName)
        {
            filePath = serializeFolderPath + "\\" + fileName + ".json";
            if (bookSerializationID != null)
            {
                List<GetBookView> booksToSerialize = new List<GetBookView>();

                List<GetBookView> booksfromFile = DeserializationExtensionClass.DeserializeJSON<GetBookView>(filePath);

                if (booksfromFile != null)
                {
                    booksToSerialize = booksfromFile;
                }

                foreach (int book in bookSerializationID.ToList())
                {
                    GetBookView bookToSerialize = bookService.GetbookByID(book); 

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
                List<GetBookView> booksToSerialize = new List<GetBookView>();

                List<GetBookView> booksFromFile = DeserializationExtensionClass.DeserializeXML<GetBookView>(filePath);
                if (booksFromFile != null)
                {
                    booksToSerialize = booksFromFile;
                }
                foreach (var book in bookSerializationID.ToList())
                {
                    GetBookView bookToSerialize = bookService.GetbookByID(book);
                    if (!booksToSerialize.Contains(bookToSerialize))
                    {
                        booksToSerialize.Add(bookToSerialize);
                    }
                }

                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<GetBookView>));
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
                        List<GetBookView> fileContent = DeserializationExtensionClass.DeserializeJSON<GetBookView>(filePath);
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
                        List<GetBookView> fileContent = DeserializationExtensionClass.DeserializeJSON<GetBookView>(filePath);

                        for (int i = 0; i < 1; i++)
                        {
                            if (fileContent == null)
                            {
                                throw new Exception("Wrong filef for this publications type. Please, choose another file");
                            }
                        }
                        
                        ViewData["BookDataXML"] = DeserializationExtensionClass.DeserializeXML<GetBookView>(filePath);
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
