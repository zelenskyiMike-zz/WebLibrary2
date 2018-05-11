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
    public class MagazineController : Controller
    {
        string serializeFolderPath;
        private string filePath;

        private Regex regexJSON;
        private Regex regexXML;

        private MatchCollection matchXML;
        private MatchCollection matchJSON;
        private readonly MagazineService magazineService;

        public MagazineController(MagazineService magazineService)
        {
            var userProfilePath = Environment.GetEnvironmentVariable("USERPROFILE");
            serializeFolderPath = Path.Combine(userProfilePath, @"source\repos\WebLibrary2\Serialization");
            this.magazineService = magazineService;
        }

        public PartialViewResult MagazinesView()
        {
            var magazines = magazineService.GetAllMagazines();
            return PartialView(magazines);
        }

        [HttpPost]
        public ActionResult SerializeMagazineToJSON(int[] magazineSerializationID, string fileName)
        {
            filePath = serializeFolderPath + "\\" + fileName + ".json";
            if (magazineSerializationID != null)
            {
                List<GetMagazineView> magazinesToSerialize = new List<GetMagazineView>();

                List<GetMagazineView> magazinesFromFile = DeserializationExtensionClass.DeserializeJSON<GetMagazineView>(filePath);
                if (magazinesFromFile != null)
                {
                    magazinesToSerialize = magazinesFromFile;
                }

                foreach (int magazine in magazineSerializationID.ToList())
                {
                    GetMagazineView magazineToSerialize = magazineService.GetMagazineByID(magazine);
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
                ViewBag.Success = true;
                return RedirectToAction("Index", "Home");
            }
            Exception nullEx = new Exception("There is nothing to serialize");
            return View("Error", new HandleErrorInfo(nullEx, "Magazine", "MagazinesView"));
        }
        [HttpPost]
        public ActionResult SerializeMagazineToXML(int[] magazineSerializationID, string fileName)
        {
            filePath = serializeFolderPath + "\\" + fileName + ".xml";
            if (magazineSerializationID != null)
            {
                List<GetMagazineView> magazinesToSerialize = new List<GetMagazineView>();

                List<GetMagazineView> magazinesFromFile = DeserializationExtensionClass.DeserializeJSON<GetMagazineView>(filePath);
                if (magazinesFromFile != null)
                {
                    magazinesToSerialize = magazinesFromFile;
                }

                foreach (var magazine in magazineSerializationID.ToList())
                {
                    GetMagazineView magazineToSerialize = magazineService.GetMagazineByID(magazine);
                    if (!magazinesToSerialize.Contains(magazineToSerialize))
                    {
                        magazinesToSerialize.Add(magazineToSerialize);
                    }
                }

                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    XmlSerializer XmlSerializer = new XmlSerializer(typeof(List<GetMagazineView>));
                    XmlSerializer.Serialize(fs, magazinesToSerialize);
                }
                ViewBag.Success = true;
                return RedirectToAction("Index", "Home");
            }
            Exception nullEx = new Exception("There is nothing to serialize");
            return View("Error", new HandleErrorInfo(nullEx, "Magazine", "MagazinesView"));
        }


        [HttpPost]
        public ActionResult DeserializeMagazine(HttpPostedFileBase file)
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
                    List<GetMagazineView> fileContent = DeserializationExtensionClass.DeserializeJSON<GetMagazineView>(filePath);
                    for (int i = 0; i < 1; i++)
                    {
                        if (fileContent[i].MagazineID == 0)
                        {
                            throw new Exception("Wrong file for this publications type. Please, choose another file");
                        }
                    }
                    ViewData["MagazineDataJSON"] = fileContent;
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Magaine", "MagainesView"));
                }
                return View();
            }
            if (matchXML.Count != 0)
            {
                try
                {
                    List<GetMagazineView> fileContent = DeserializationExtensionClass.DeserializeXML<GetMagazineView>(filePath);
                    for (int i = 0; i < 1; i++)
                    {
                        if (fileContent == null)
                        {
                            throw new Exception("Wrong file for this publications type. Please, choose another file");
                        }
                    }
                    ViewData["MagazineDataXML"] = fileContent;
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Magaine", "MagainesView"));
                }
                return View();
            }
            Exception nullEx = new Exception("File is null");
            return View("Error", new HandleErrorInfo(nullEx, "Magazine", "MagazinesView"));
        }
    }
}