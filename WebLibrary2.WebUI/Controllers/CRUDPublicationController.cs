using System.Net;
using System.Web.Mvc;
using WebLibrary2.BusinessLogicLayer.Sevices;
using WebLibrary2.ViewModelsLayer.ViewModels;

namespace WebLibrary2.WebUI.Controllers
{
    public class CRUDPublicationController : Controller
    {
        private readonly PublicationService publicationService;
        private readonly AuthorService authorService;

        public CRUDPublicationController(PublicationService publicationService,AuthorService authorService)
        {
            this.publicationService = publicationService;
            this.authorService = authorService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult CreatePublication()
        {
            SelectList genres = new SelectList(publicationService.GetAllPublicationGenres(), "PublicationGenreID", "PublicationGenreName");
            MultiSelectList authors = new MultiSelectList(authorService.GetAllAuthors(), "AuthorID", "AuthorName");
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
                publicationService.CreatePublication(publicationVM);
                return RedirectToAction("Index", "Home");
            }
            SelectList genres = new SelectList(publicationService.GetAllPublicationGenres(), "PublicationGenreID", "PublicationGenreName");
            MultiSelectList authors = new MultiSelectList(authorService.GetAllAuthors(), "AuthorID", "AuthorName");
            ViewData["PublicationGenres"] = genres;
            ViewData["Authors"] = authors;
            return View(publicationVM);
        }


        [Authorize(Roles = "user,admin")]
        [HttpGet]
        public ActionResult PublicationDetails(int id)
        {
            var publicationVM = publicationService.GetPublicationDetails(id);
            return View(publicationVM);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult EditPublication(int id)
        {
            var publication = publicationService.GetPublicationDetails(id);

            if (publication == null)
            {
                HttpNotFound();
            }

            SelectList genres = new SelectList(publicationService.GetAllPublicationGenres(), "PublicationGenreID", "PublicationGenreName", publication.PublicationGenreID);
            ViewData["PublicationGenres"] = genres;

            MultiSelectList authors = new MultiSelectList(publicationService.GetAuthorsNotExistInPublication(publication), "AuthorID", "AuthorName", publication.Authors);
            ViewData["Authors"] = authors;

            return View("EditPublication", publication);
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

            if (TryUpdateModel(publicationVM))
            {
                publicationService.EditPublication(publicationVM, authorIDsForDelete, authorIDsForInsert);
                return new RedirectResult(Url.Action("Index", "Home", new {tab = "publications" }));
            }

            var publication = publicationService.GetPublicationDetails(publicationVM.PublicationID);


            SelectList genres = new SelectList(publicationService.GetAllPublicationGenres(), "PublicationGenreID", "PublicationGenreName", publication.PublicationGenreID);
            ViewData["PublicationGenres"] = genres;

            MultiSelectList authors = new MultiSelectList(publicationService.GetAuthorsNotExistInPublication(publication), "AuthorID", "AuthorName", publication.Authors);
            ViewData["Authors"] = authors;

            return View("EditPublication", publication);

        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult DeletePublication(int id)
        {
            var publicationVM = publicationService.GetPublicationDetails(id);
            if (publicationVM == null)
            {
                HttpNotFound();
            }
            return View("DeletePublication", publicationVM);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePublication(GetAllPublicationsView publicationVM)
        {
            publicationService.DeletePublication(publicationVM);
            return RedirectToAction("Index", "Home");
        }
    }
}