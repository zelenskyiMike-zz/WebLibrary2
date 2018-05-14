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
    public class PublicationsController : Controller
    {
        string serializeFolderPath;
        private string filePath;

        private Regex regexJSON;
        private Regex regexXML;

        private MatchCollection matchXML;
        private MatchCollection matchJSON;
        private readonly PublicationService publicationService;

        public PublicationsController(PublicationService publicationService)
        {
            var userProfilePath = Environment.GetEnvironmentVariable("USERPROFILE");
            serializeFolderPath = Path.Combine(userProfilePath, @"source\repos\WebLibrary2\Serialization");
            this.publicationService = publicationService;
        }
        public PartialViewResult PublicationsView()
        {
            var publications = publicationService.GetAllPubicationsWithGenres();
            return PartialView(publications);
        }

        [HttpPost]
        public ActionResult SerializePublicationToJSON(int[] publicationSerializationID,string fileName)
        {
            filePath = serializeFolderPath +"\\"+ fileName + ".json";
            if (publicationSerializationID != null)
            {
                List<GetPublicationView> publicationsToSerialize = new List<GetPublicationView>();
                List<GetPublicationView> publicationsFromFile = DeserializationExtensionClass.DeserializeJSON<GetPublicationView>(filePath);

                if (publicationsFromFile != null)
                {
                    publicationsToSerialize = publicationsFromFile;
                }

                foreach (int publication in publicationSerializationID.ToList())
                {
                    GetPublicationView publicationToSerialize = publicationService.GetPublicationByID(publication);
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
                ViewData["Message"] = "Successfully Serialized";
                return RedirectToAction("Index", "Home");
            }
            ViewData["Message"] = null;
            Exception nullEx = new Exception("There is nothing to serialize");
            return View("Error", new HandleErrorInfo(nullEx, "Publications", "PublicationsView"));
        }



        [HttpPost]
        public ActionResult SerializePublicationToXML(int[] publicationSerializationID, string fileName)
        {
            filePath = serializeFolderPath + "\\" + fileName + ".xml";
            if (publicationSerializationID != null)
            {
                List<GetPublicationView> publicationsToSerialize = new List<GetPublicationView>();
                List<GetPublicationView> publicationsFromFile = DeserializationExtensionClass.DeserializeXML<GetPublicationView>(filePath);


                if (publicationsFromFile != null)
                {
                    publicationsToSerialize = publicationsFromFile;
                }

                foreach (var publication in publicationSerializationID.ToList())
                {
                    GetPublicationView publicationToSerialize = publicationService.GetPublicationByID(publication);
                    if (!publicationsToSerialize.Contains(publicationToSerialize))
                    {
                        publicationsToSerialize.Add(publicationToSerialize);
                    }
                }

                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<GetPublicationView>));
                    XmlSerializer.Serialize(fs, publicationsToSerialize);
                }
                return RedirectToAction("Index", "Home");
            }
            Exception nullEx = new Exception("There is nothing to serialize");
            return View("Error", new HandleErrorInfo(nullEx, "Publications", "PublicationsView"));
        }

        [HttpPost]
        public ActionResult DeserializePublication(HttpPostedFileBase file)
        {
            regexJSON = new Regex(@"(\w*).json");
            regexXML = new Regex(@"(\w*).xml");


            if (file != null)
            {
                filePath = FilePath.GetFilePath(file, serializeFolderPath);

                matchJSON = regexJSON.Matches(filePath);
                matchXML = regexXML.Matches(filePath);
            }

            if (matchJSON.Count != 0)
            {
                try
                {
                    List<GetPublicationView> fileContent = DeserializationExtensionClass.DeserializeJSON<GetPublicationView>(filePath);
                    for (int i = 0; i < 1; i++)
                    {
                        if (fileContent[i].PublicationID == 0)
                        {
                            throw new Exception("Wrong file for this publications type. Please, choose another file");
                        }
                    }
                    ViewData["PublicationDataJSON"] = fileContent;
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Publications", "PublicationsView"));
                }
                return View();
            }
            if (matchXML.Count != 0)
            {
                try
                {
                    List<GetPublicationView> fileContent = DeserializationExtensionClass.DeserializeXML<GetPublicationView>(filePath);
                    for (int i = 0; i < 1; i++)
                    {
                        if (fileContent == null)
                        {
                            throw new Exception("Wrong file for this publications type. Please, choose another file");
                        }
                    }
                    ViewData["PublicationDataXML"] = fileContent;
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Publications", "PublicationsView"));
                }
                return View();
            }
            Exception nullEx = new Exception("File is null");
            return View("Error", new HandleErrorInfo(nullEx, "Publications", "PublicationsView"));
        }
    }
}