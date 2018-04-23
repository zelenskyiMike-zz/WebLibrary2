using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebLibrary2.Domain.Abstract.AbstractMagazine;
using WebLibrary2.Domain.Entity.MagazineEntity;
using WebLibrary2.Domain.Extensions;

namespace WebLibrary2.WebUI.Controllers.MagazineControllers
{
    public class MagazineController : Controller
    {
        string serializeFolderPath;
        private string filePath;

        private Regex regexJSON;
        private Regex regexXML;
        private Regex regexValidation;

        private MatchCollection matchXML;
        private MatchCollection matchJSON;
        private MatchCollection matchValidation;

        IMagazineRepository magazineRepository;

        public MagazineController(IMagazineRepository magazinesRepository)
        {
            magazineRepository = magazinesRepository;

            var userProfilePath = Environment.GetEnvironmentVariable("USERPROFILE");
            serializeFolderPath = Path.Combine(userProfilePath, @"source\repos\WebLibrary2\Serialization");
        }
        // GET: Magazine
        public PartialViewResult MagazinesView()
        {
            var magazines = magazineRepository.GetAllMagazines();
            return PartialView(magazines);
        }

        [HttpPost]
        public ActionResult DeserializeMagazine(HttpPostedFileBase file)
        {
            regexJSON = new Regex(@"(\w*).json");
            regexXML = new Regex(@"(\w*).xml");
            regexValidation = new Regex(@"(\w*)Magazine(\w*)");


            if (file != null)
            {
                filePath = FilePath.GetFilePath(file, serializeFolderPath);

                matchJSON = regexJSON.Matches(filePath);
                matchXML = regexXML.Matches(filePath);
                matchValidation = regexValidation.Matches(filePath);
            }

            if (matchJSON.Count != 0)
            {
                try
                {
                    if (matchValidation.Count == 0)
                    {
                        throw new Exception("Wrong file for this publications type. Please, choose another file");
                    }
                    ViewData["MagazineDataJSON"] = DeserializationExtensionClass.DeserializeJSON<Magazine>(filePath);
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
                    if (matchValidation.Count == 0)
                    {
                        throw new Exception("Wrong file for this publications type. Please, choose another file");
                    }
                    ViewData["MagazineDataXML"] = DeserializationExtensionClass.DeserializeXML<Magazine>(filePath);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Magaine", "MagainesView"));
                }
                return View();
            }
            throw new Exception("File is null");
        }
    }
}