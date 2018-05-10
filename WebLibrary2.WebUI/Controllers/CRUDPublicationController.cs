using System.Net;
using System.Web.Mvc;
using WebLibrary2.ViewModelsLayer.ViewModels;

namespace WebLibrary2.WebUI.Controllers
{
    public class CRUDPublicationController : Controller
    {
        //EFDbContext context;
        //EFPublicationRepository publicationRepository;
        //EFPublicationAuthorsRepository publicationAuthorsRepository;


        public CRUDPublicationController(/*EFDbContext contextParam, EFPublicationRepository publicationsRepository, EFPublicationAuthorsRepository publicationsAuthorsRepository*/)
        {
            //context = contextParam;
            //publicationRepository = publicationsRepository;
            //publicationAuthorsRepository = publicationsAuthorsRepository;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult CreatePublication()
        {
            SelectList genres = new SelectList(context.PublicationGenres, "PublicationGenreID", "PublicationGenreName");
            MultiSelectList authors = new MultiSelectList(context.Authors, "AuthorID", "AuthorName");
            ViewData["PublicationGenres"] = genres;
            ViewData["Authors"] = authors;
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePublication(GetPublicationView publicationVM)
        {
            if (ModelState.IsValid)
            {
                publicationRepository.InsertPublication(publicationVM);
                return RedirectToAction("Index", "Home");
            }
            SelectList genres = new SelectList(context.PublicationGenres, "PublicationGenreID", "PublicationGenreName");
            MultiSelectList authors = new MultiSelectList(context.Authors, "AuthorID", "AuthorName");
            ViewData["PublicationGenres"] = genres;
            ViewData["Authors"] = authors;
            return View(publicationVM);
        }


        [Authorize(Roles = "user,admin")]
        [HttpGet]
        public ActionResult PublicationDetails(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var publicationVM = publicationRepository.GetPublicationDetails(id);
            return View(publicationVM);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult EditPublication(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var publication = publicationRepository.GetPublicationDetails(id);

            if (publication == null)
            {
                HttpNotFound();
            }

            SelectList genres = new SelectList(context.PublicationGenres, "PublicationGenreID", "PublicationGenreName", publication.PublicationGenreID);
            ViewData["PublicationGenres"] = genres;

            MultiSelectList authors = new MultiSelectList(publicationRepository.GetAuthorsNotExistInPublication((int)id), "AuthorID", "AuthorName", publication.Authors);
            ViewData["Authors"] = authors;

            return View(publication);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPublication(GetPublicationView publicationVM, int[] authorIDsForDelete, int[] authorIDsForInsert)
        {
            if (publicationVM == null)
            {
                HttpNotFound();
            }

            var publicationToUpdate = publicationRepository.GetPublicationByID(publicationVM.PublicationID);
            ViewBag.ActiveTab = publicationToUpdate.PublicationID.ToString();

            if (TryUpdateModel(publicationToUpdate))
            {
                publicationAuthorsRepository.DeleteAuthorFromPublication(publicationToUpdate.PublicationID, authorIDsForDelete);
                publicationAuthorsRepository.AddAuthorToPublication(publicationToUpdate.PublicationID, authorIDsForInsert);
                publicationRepository.Save();
                return new RedirectResult(Url.Action("Index", "Home", new {tab = "publications" }));
            }

            var publication = publicationRepository.GetPublicationDetails(publicationVM.PublicationID);


            SelectList genres = new SelectList(context.PublicationGenres, "PublicationGenreID", "PublicationGenreName", publication.PublicationGenreID);
            ViewData["PublicationGenres"] = genres;

            MultiSelectList authors = new MultiSelectList(publicationRepository.GetAuthorsNotExistInPublication((int)publication.PublicationID), "AuthorID", "AuthorName", publication.Authors);
            ViewData["Authors"] = authors;

            return View("EditPublication", publication);

        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult DeletePublication(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            GetPublicationView publicationVM = publicationRepository.GetPublicationDetails(id);
            if (publicationVM == null)
            {
                HttpNotFound();
            }
            return View(publicationVM);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePublication(int id)
        {
            publicationRepository.DeletePublication(id);
            return RedirectToAction("Index", "Home");
        }
    }
}