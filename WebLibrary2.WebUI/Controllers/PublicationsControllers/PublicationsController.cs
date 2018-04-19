using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebLibrary2.Domain.Abstract.AbstractPublication;
using WebLibrary2.Domain.Entity.PublicationEntity;
using WebLibrary2.Domain.Extensions;

namespace WebLibrary2.WebUI.Controllers.PublicationsControllers
{
    public class PublicationsController : Controller
    {
        string serializeFolderPath;
        private string filePath;

        private MatchCollection matchXML;
        private MatchCollection matchJSON;
        private Regex regexJSON;
        private Regex regexXML;

        IPublicationRepository publicationRepository;

        public PublicationsController(IPublicationRepository publicationsRepository)
        {
            publicationRepository = publicationsRepository;

            var userProfilePath = Environment.GetEnvironmentVariable("USERPROFILE");
            serializeFolderPath = Path.Combine(userProfilePath, @"source\repos\WebLibrary2\Serialization");
        }
        public PartialViewResult PublicationsView()
        {
            var publications = publicationRepository.GetAllPublications();
            return PartialView(publications);
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
                    ViewData["MagazineDataJSON"] = DeserializationExtensionClass.DeserializeJSON<Publication>(filePath);
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
                    ViewData["MagazineDataXML"] = DeserializationExtensionClass.DeserializeXML<Publication>(filePath);
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