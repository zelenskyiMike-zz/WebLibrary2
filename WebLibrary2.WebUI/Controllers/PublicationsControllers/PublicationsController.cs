using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebLibrary2.Domain.Abstract.AbstractPublication;
using WebLibrary2.Domain.Concrete.ConcretePublication;
using WebLibrary2.Domain.Entity.PublicationEntity;
using WebLibrary2.Domain.Extensions;

namespace WebLibrary2.WebUI.Controllers.PublicationsControllers
{
    public class PublicationsController : Controller
    {
        string serializeFolderPath;
        private string filePath;

        private Regex regexJSON;
        private Regex regexXML;
        private Regex regexValidation;

        private MatchCollection matchXML;
        private MatchCollection matchJSON;
        private MatchCollection matchValidation;

        EFPublicationRepository publicationRepository;

        public PublicationsController(EFPublicationRepository publicationsRepository)
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
            regexValidation = new Regex(@"(\w*)Publication(\w*)");


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
                    ViewData["PublicationDataJSON"] = DeserializationExtensionClass.DeserializeJSON<Publication>(filePath);
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
                    if (matchValidation.Count == 0)
                    {
                        throw new Exception("Wrong file for this publications type. Please, choose another file");
                    }
                    ViewData["PublicationDataXML"] = DeserializationExtensionClass.DeserializeXML<Publication>(filePath);
                }
                catch (Exception ex)
                {
                    return View("Error", new HandleErrorInfo(ex, "Publications", "PublicationsView"));
                }
                return View();
            }
            throw new Exception("File is null");
        }
    }
}