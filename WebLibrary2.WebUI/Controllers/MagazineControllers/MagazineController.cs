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

        private MatchCollection matchXML;
        private MatchCollection matchJSON;
        private Regex regexJSON;
        private Regex regexXML;

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
                    ViewData["MagazineDataJSON"] = DeserializationExtensionClass.DeserializeJSON<Magazine>(filePath);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return View();
            }
            if (matchXML.Count != 0)
            {
                try
                {
                    ViewData["MagazineDataXML"] = DeserializationExtensionClass.DeserializeXML<Magazine>(filePath);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return View();
            }
            throw new Exception("File is null");
        }
    }
}