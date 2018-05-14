using System.Net;
using System.Web.Mvc;
using WebLibrary2.BusinessLogicLayer.Sevices;
using WebLibrary2.ViewModelsLayer.ViewModels;

namespace WebLibrary2.WebUI.Controllers
{
    public class CRUDMagazineController : Controller
    {
        private readonly MagazineService magazineService;
        private readonly AuthorService authorService;

        public CRUDMagazineController(MagazineService magazineService, AuthorService authorService)
        {
            this.magazineService = magazineService;
            this.authorService = authorService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult CreateMagazine()
        {
            SelectList genres = new SelectList(magazineService.GetAllGenres(), "MagazineGenreID", "MagazineGenreName");
            MultiSelectList authors = new MultiSelectList(authorService.GetAllAuthors(), "AuthorID", "AuthorName");
            ViewData["MagazineGenres"] = genres;
            ViewData["Authors"] = authors;
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateMagazine(GetMagazineView magazineVM)
        {
            if (ModelState.IsValid)
            {
                magazineService.CreateMagazine(magazineVM);
                return RedirectToAction("Index", "Home");
            }
            SelectList genres = new SelectList(magazineService.GetAllGenres(), "MagazineGenreID", "MagazineGenreName");
            MultiSelectList authors = new MultiSelectList(authorService.GetAllAuthors(), "AuthorID", "AuthorName");
            ViewData["MagazineGenres"] = genres;
            ViewData["Authors"] = authors;
            return View(magazineVM);
        }


        [Authorize(Roles = "user,admin")]
        [HttpGet]
        public ActionResult MagazineDetails(int id)
        {
            var magazineVM = magazineService.GetMagazineDetails(id);
            return View(magazineVM);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult EditMagazine(int id)
        {
            var magazine = magazineService.GetMagazineDetails(id);

            if (magazine == null)
            {
                HttpNotFound();
            }

            SelectList genres = new SelectList(magazineService.GetAllGenres(), "MagazineGenreID", "MagazineGenreName", magazine.MagazineGenreID);
            ViewData["MagazineGenres"] = genres;

            MultiSelectList authors = new MultiSelectList(magazineService.GetAuthorsNotExistInMagazine(magazine), "AuthorID", "AuthorName", magazine.Authors);
            ViewData["Authors"] = authors;

            return View("EditMagazine",magazine);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMagazine(GetAllMagazinesView magazineVM, int[] authorIDsForDelete, int[] authorIDsForInsert)
        {
            if (magazineVM == null)
            {
                HttpNotFound();
            }

            if (TryUpdateModel(magazineVM))
            {
                magazineService.EditMagazine(magazineVM, authorIDsForDelete, authorIDsForInsert);
                return RedirectToAction("Index", "Home");
            }

            SelectList genres = new SelectList(magazineService.GetAllGenres(), "MagazineGenreID", "MagazineGenreName", magazineVM.MagazineGenreID);
            ViewData["MagazineGenres"] = genres;

            MultiSelectList authors = new MultiSelectList(magazineService.GetAuthorsNotExistInMagazine(magazineVM), "AuthorID", "AuthorName", magazineVM.Authors);
            ViewData["Authors"] = authors;

            return View("EditMagazine", magazineVM);
        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult DeleteMagazine(int id)
        {
            var magazineVM = magazineService.GetMagazineDetails(id);
            if (magazineVM == null)
            {
                HttpNotFound();
            }
            return View(magazineVM);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteMagazine(int? id)
        {
            magazineService.DeleteMagazine((int)id);
            return RedirectToAction("Index", "Home");
        }
    }
}